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
  public class DiaryTopicController : Controller
  {
    private readonly UserManager<BeonUser> _userManager;
    private IRepository<Topic> _topicRepository;
    private TopicLogic _topicLogic;
    private IRepository<Diary> _diaryRepository;
    private IRepository<OriginalPost> _opRepository;
    private LinkGenerator _linkGenerator;
    private readonly ILogger _logger;
    private readonly TopicSubscriptionLogic _topicSubscriptionLogic;
    public DiaryTopicController(
      IRepository<Topic> topicRepository,
      TopicLogic topicLogic,
      IRepository<Diary> diaryRepository,
      IRepository<OriginalPost> opRepository,
      UserManager<BeonUser> userManager,
      LinkGenerator linkGenerator,
      ILogger<DiaryTopicController> logger,
      TopicSubscriptionLogic topicSubscriptionLogic)
    {
      _topicRepository = topicRepository;
      _diaryRepository = diaryRepository;
      _opRepository = opRepository;
      _userManager = userManager;
      _logger = logger;
      _linkGenerator = linkGenerator;
      _topicSubscriptionLogic = topicSubscriptionLogic;
      _topicLogic = topicLogic;
    }

    [HttpPost]
    [Authorize]
    [Route("/diary/{userName:required}/CreateTopic")]
    public async Task<IActionResult> Create(string userName, TopicFormModel model) {
      BeonUser? user = await _userManager.GetUserAsync(User);
      
      if (user == null)
      {
        return NotFound();
      }

      BeonUser? diaryOwner = await _userManager.Users
        .Where(u => u.UserName.Equals(userName))
        .Include(u => u.Diary)
        .FirstOrDefaultAsync();
      
      if (diaryOwner == null)
      {
        return NotFound();
      }

      if (diaryOwner.Diary == null)
      {
        throw new Exception("invalid user: has no diary");
      }

      Diary d = diaryOwner.Diary;

      if (ModelState.IsValid)
      {
        int topicOrd = d.TopicCounter++;
        await _diaryRepository.UpdateAsync(d);
        DateTime timeStamp = DateTime.UtcNow;
        Topic topic = new Topic
        {
          Title = model.Title,
          BoardId = d.BoardId,
          TopicOrd = topicOrd,
          TimeStamp = timeStamp,
          Poster = user
        };
        await _topicRepository.CreateAsync(topic);
        OriginalPost op = new OriginalPost
        {
          Body = model.Op.Body,
          Poster = user,
          Topic = topic,
          TimeStamp = timeStamp
        };
        await _opRepository.CreateAsync(op);
        await _topicSubscriptionLogic.SubscribeAsync(topic.TopicId, user.Id);
        return RedirectToAction("Show", "DiaryTopic", new { userName = userName, topicOrd = topicOrd });
      }
      else
      {
        return NotFound();
      }
    }

    [Route("/diary/{userName:required}/0-{topicOrd:int}")]
    public async Task<IActionResult> Show(string userName, int topicOrd) {
      BeonUser? diaryOwner = await _userManager.Users
        .Where(u => u.UserName.Equals(userName))
        .Include(u => u.Diary)
        .FirstOrDefaultAsync();
      
      if (diaryOwner == null)
      {
        return NotFound();
      }

      if (diaryOwner.Diary == null)
      {
        throw new Exception("invalid user: has no diary");
      };

      Topic? t = await _topicRepository.Entities
        .Where(t => t.BoardId.Equals(diaryOwner.Diary.BoardId) && t.TopicOrd.Equals(topicOrd))
        .FirstOrDefaultAsync();

      if (t == null) {
        return RedirectToAction("Show", "Diary", new { userName = userName });
      }

      string? postCreatePath = _linkGenerator.GetPathByAction("Create", "DiaryPost", new { userName = userName, topicOrd = topicOrd });  

      if (postCreatePath == null) {
        throw new Exception("Couldn't generate post creation path");
      }

      BeonUser? user = await _userManager.GetUserAsync(User);
      bool canEdit = false;
      if (user != null) {
        await _topicSubscriptionLogic.UnsetNewCommentsAsync(t.TopicId, user.Id);
        canEdit = await _topicLogic.UserMayEditTopicAsync(t, user);
      }

      PostViewModel op = await _topicLogic.GetOpAsync(t.TopicId);
      ICollection<CommentViewModel> comments = await _topicLogic.GetCommentsAsync(t.TopicId, user);

      ViewBag.IsDiaryPage = true;
      ViewBag.DiaryTitle = diaryOwner.DisplayName;
      ViewBag.DiarySubtitle = diaryOwner.DisplayName;
      ViewBag.HrBarViewModel = new HrBarViewModel(
        crumbs: new List<LinkViewModel>
        {
          new LinkViewModel(diaryOwner.DisplayName, _linkGenerator.GetPathByAction("Show", "Diary", new { userName = userName }) ?? "error"),
          await _topicLogic.GetShortLinkAsync(t)
        },
        timeStamp: t.TimeStamp);
      
      return View(new DiaryTopicViewModel(userName, new TopicViewModel(postCreatePath, t.TopicId, t.Title, op, comments, canEdit)));
    }
  }
}