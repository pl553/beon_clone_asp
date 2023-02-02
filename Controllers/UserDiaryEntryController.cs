using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Services;
using Beon.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Beon.Infrastructure;

namespace Beon.Controllers
{
  public class UserDiaryEntryController : Controller
  {
    private readonly UserManager<BeonUser> _userManager;
    private readonly IRepository<UserDiaryEntry> _userDiaryEntryRepository;
    private readonly TopicSubscriptionService _topicSubscriptionService;
    private readonly LinkGenerator _linkGenerator;
    private readonly BeonDbContext _context;

    public UserDiaryEntryController(
      BeonDbContext context,
      IRepository<UserDiaryEntry> userDiaryEntryRepository,
      UserManager<BeonUser> userManager,
      LinkGenerator linkGenerator,
      TopicSubscriptionService topicSubscriptionService)
    {
      _context = context;
      _userDiaryEntryRepository = userDiaryEntryRepository;
      _userManager = userManager;
      _linkGenerator = linkGenerator;
      _topicSubscriptionService = topicSubscriptionService;
    }

    [Route("/diary/{userName:required}/0-{topicOrd:int}")]
    public async Task<IActionResult> Show(string userName, int topicOrd) {
      var diaryOwner = await _userManager.Users
        .Where(u => u.UserName.Equals(userName))
        .Include(u => u.Diary)
        .FirstOrDefaultAsync();
      
      if (diaryOwner == null)
      {
        return NotFound();
      }

      var entry = await _userDiaryEntryRepository.Entities
        .Where(e => e.PosterId == diaryOwner.Id && e.TopicOrd == topicOrd)
        .FirstOrDefaultAsync();

      if (entry == null)
      {
        return RedirectToAction("Show", "UserDiary", new { userName = userName });
      }

      var user = await _userManager.GetUserAsync(User);

      if (!await entry.UserCanReadAsync(user))
      {
        return NotFound();
      }
      
      if (user != null)
      {
        await _topicSubscriptionService.UnsetNewCommentsAsync(entry.PostId, user.Id);
      }
      
      ViewBag.IsDiaryPage = true;
      ViewBag.DiaryTitle = diaryOwner.DisplayName;
      ViewBag.DiarySubtitle = diaryOwner.DisplayName;
      ViewBag.HrBarViewModel = new HrBarViewModel(
        crumbs: new List<LinkViewModel>
        {
          new LinkViewModel(
            diaryOwner.DisplayName,
            await (await diaryOwner.GetDiaryAsync()).GetPathAsync()),
          new LinkViewModel(
            entry.Title,
            await entry.GetPathAsync())
        },
        timeStamp: entry.TimeStamp);

      return View(new UserDiaryEntryPageViewModel(
        diaryOwner.UserName,
        await UserDiaryEntryViewModel.CreateFromAsync(entry, user)));
    }

    // GET method
    [HttpGet]
    [Route("/diary/{userName:required}/0-{topicOrd:int}/Edit")]
    public async Task<IActionResult> Edit(string userName, int topicOrd) 
    {
      var diaryOwner = await _userManager.Users
        .Where(u => u.UserName.Equals(userName))
        .Include(u => u.Diary)
        .FirstOrDefaultAsync();

      var entry = await _userDiaryEntryRepository.Entities
        .Where(e => e.PosterId == diaryOwner.Id && e.TopicOrd == topicOrd)
        .FirstOrDefaultAsync();

      var user = await _userManager.GetUserAsync(User);

       if (entry == null || user == null || user.Id != diaryOwner.Id)
      {
        return RedirectToAction("Index", "Home");
      }

      return View(new UserDiaryEntryEditPageViewModel (
        diaryOwner.UserName,
        new UserDiaryEntryFormModel { 
          Body = entry.Body, 
          Title = entry.Title, 
          ReadAccess = entry.ReadAccess,
          CommentAccess = entry.CommentAccess 
        } ));

    }

    // POST method
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    [Route("/diary/{userName:required}/0-{topicOrd:int}/Edit")]
    public async Task<IActionResult> Edit(UserDiaryEntryFormModel model, int topicOrd) 
    {
      if (!ModelState.IsValid)
      {
        return NotFound();
      }

      var user = await _userManager.GetUserAsync(User);
      var diaryOwner = await _userManager.Users
        .Where(u => u.UserName == model.DiaryOwnerUserName)
        .FirstOrDefaultAsync();

      if (user == null || user.Id != diaryOwner?.Id)
      {
        return RedirectToAction("Index", "Home");
      }
      
      if (model.ReadAccess == UserDiaryEntry.Access.Invalid
        || model.CommentAccess == UserDiaryEntry.Access.Invalid)
      {
        return RedirectToAction("Index", "Home");
      }

      var entry = await _userDiaryEntryRepository.Entities
        .Where(e => e.PosterId == diaryOwner.Id && e.TopicOrd == topicOrd)
        .FirstOrDefaultAsync();

      if (entry == null)
      {
        return RedirectToAction("Index", "Home");
      }
      

      entry.Body = model.Body;
      entry.Title = model.Title ?? Topic.GenerateTitleFromBody(model.Body);
      entry.CommentAccess = model.CommentAccess;
      entry.ReadAccess = model.ReadAccess;

      await _userDiaryEntryRepository.UpdateAsync(entry);
      await _topicSubscriptionService.SubscribeAsync(entry.PostId, user.Id);

      return this.RedirectToLocal(await entry.GetPathAsync());
    }


    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserDiaryEntryFormModel model)
    {
      if (!ModelState.IsValid)
      {
        return NotFound();
      }

      var user = await _userManager.GetUserAsync(User);
      var diaryOwner = await _userManager.Users
        .Where(u => u.UserName == model.DiaryOwnerUserName)
        .FirstOrDefaultAsync();

      if (user == null || user.Id != diaryOwner?.Id)
      {
        return NotFound();
      }

      if (model.ReadAccess == UserDiaryEntry.Access.Invalid
        || model.CommentAccess == UserDiaryEntry.Access.Invalid)
      {
        return NotFound();
      }
      
      var entry = new UserDiaryEntry(
        context: _context,
        body: model.Body,
        timeStamp: DateTime.UtcNow,
        posterId: user.Id,
        model.Title == null || model.Title.Length == 0 ? Topic.GenerateTitleFromBody(model.Body) : model.Title,
        topicOrd: await (await user.GetDiaryAsync()).GetNextTopicOrdAsync(),
        userDiaryId: (await user.GetDiaryAsync()).DiaryId,
        readAccess: model.ReadAccess,
        commentAccess: model.CommentAccess,
        desires: "",
        mood: "",
        music: ""
      );

      await _userDiaryEntryRepository.CreateAsync(entry);
      await _topicSubscriptionService.SubscribeAsync(entry.PostId, user.Id);

      return this.RedirectToLocal(await entry.GetPathAsync());
    }
  }
}