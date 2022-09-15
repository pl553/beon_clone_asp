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
    private IRepository<Board> _boardRepository;
    private IRepository<OriginalPost> _opRepository;
    private LinkGenerator _linkGenerator;
    private readonly ILogger _logger;
    private readonly TopicSubscriptionLogic _topicSubscriptionLogic;
    public DiaryTopicController(
      IRepository<Topic> topicRepository,
      TopicLogic topicLogic,
      IRepository<Board> boardRepository,
      IRepository<OriginalPost> opRepository,
      UserManager<BeonUser> userManager,
      LinkGenerator linkGenerator,
      ILogger<DiaryTopicController> logger,
      TopicSubscriptionLogic topicSubscriptionLogic)
    {
      _topicRepository = topicRepository;
      _boardRepository = boardRepository;
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
      string loggedInUserName = await _userManager.GetUserNameAsync(await _userManager.GetUserAsync(User));

      if (loggedInUserName != userName) {
        return NotFound();
      }
      
      Board? board = await _boardRepository.Entities
        .Where(b => b.OwnerName.Equals(userName) && b.Type.Equals(BoardType.Diary))
        .FirstOrDefaultAsync();

      if (board == null) {
        return NotFound();
      }
      
      BeonUser? u = await _userManager.GetUserAsync(User);

      if (u == null) {
        return NotFound();
      }

      if (ModelState.IsValid) {
        int topicOrd = board.topicCounter++;
        await _boardRepository.UpdateAsync(board);
        DateTime timeStamp = DateTime.UtcNow;
        Topic topic = new Topic
        {
          Title = model.Title,
          BoardId = board.BoardId,
          TopicOrd = topicOrd,
          TimeStamp = timeStamp,
          Poster = u
        };
        await _topicRepository.CreateAsync(topic);
        OriginalPost op = new OriginalPost
        {
          Body = model.Op.Body,
          Poster = u,
          Topic = topic,
          TimeStamp = timeStamp
        };
        await _opRepository.CreateAsync(op);
        await _topicSubscriptionLogic.SubscribeAsync(topic.TopicId, u.Id);
        return RedirectToAction("Show", "DiaryTopic", new { userName = userName, topicOrd = topicOrd});
      }
      else {
        return NotFound();
      }
    }

    [Route("/diary/{userName:required}/0-{topicOrd:int}")]
    public async Task<IActionResult> Show(string userName, int topicOrd) {
      string? displayName = await _userManager.Users
        .Where(u => u.UserName.Equals(userName))
        .Select(u => u.DisplayName)
        .FirstOrDefaultAsync();
      
      if (displayName == null) {
        return NotFound();
      }

      int boardId = await _boardRepository.Entities
        .Where(b => b.OwnerName.Equals(userName) && b.Type.Equals(BoardType.Diary))
        .Select(b => b.BoardId)
        .FirstOrDefaultAsync();
      
      if (boardId == 0) {
        return NotFound();
      }

      Topic? t = await _topicRepository.Entities
        .Where(t => t.BoardId.Equals(boardId) && t.TopicOrd.Equals(topicOrd))
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
      ViewBag.DiaryTitle = displayName;
      ViewBag.DiarySubtitle = displayName;
      ViewBag.HrBarViewModel = new HrBarViewModel(
        crumbs: new List<LinkViewModel>
        {
          new LinkViewModel(displayName, _linkGenerator.GetPathByAction("Show", "Diary", new { userName = userName }) ?? "error"),
          await _topicLogic.GetShortLinkAsync(t)
        },
        timeStamp: t.TimeStamp);
      
      return View(new DiaryTopicViewModel(userName, new TopicViewModel(postCreatePath, t.TopicId, t.Title, op, comments, canEdit)));
    }
  }
}