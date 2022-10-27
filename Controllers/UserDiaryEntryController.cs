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
    private readonly IRepository<Topic> _topicRepository;
    private readonly IRepository<UserDiary> _userDiaryRepository;
    private readonly IRepository<UserDiaryEntry> _userDiaryEntryRepository;
    private readonly TopicSubscriptionService _topicSubscriptionService;
    private readonly UserDiaryEntryService _userDiaryEntryService;
    private readonly LinkGenerator _linkGenerator;

    public UserDiaryEntryController(
      IRepository<UserDiaryEntry> userDiaryEntryRepository,
      UserManager<BeonUser> userManager,
      LinkGenerator linkGenerator,
      TopicSubscriptionService topicSubscriptionService,
      UserDiaryEntryService userDiaryEntryService)
    {
      _userDiaryEntryRepository = userDiaryEntryRepository;
      _userManager = userManager;
      _linkGenerator = linkGenerator;
      _topicSubscriptionService = topicSubscriptionService;
      _userDiaryEntryService = userDiaryEntryService;
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
        return NotFound();
      }

      var user = await _userManager.GetUserAsync(User);

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
            _linkGenerator.GetPathByAction(
              "Show",
              "Diary",
              new { userName = userName })
              ?? throw new Exception("couldnt generate path to diary")),
          new LinkViewModel(
            entry.Title,
            await entry.GetPathAsync())
        },
        timeStamp: entry.TimeStamp);

      return View(new UserDiaryEntryPageViewModel(
        diaryOwner.UserName,
        await UserDiaryEntryViewModel.CreateFromAsync(entry, user)));
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
      
      try
      {
        var entry = await _userDiaryEntryService.CreateFromAsync(model, user);
        return this.RedirectToLocal(await entry.GetPathAsync());
      }
      catch
      {
        throw;
      }
    }
  }
}