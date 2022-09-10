using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Beon.Services;

namespace Beon.Controllers
{
  public class DiaryController : Controller
  {
    private readonly ILogger _logger;
    private readonly UserManager<BeonUser> _userManager;
    private readonly SignInManager<BeonUser> _signInManager;
    private readonly TopicLogic _topicLogic;
    private readonly IRepository<Board> _boardRepository;
    private readonly LinkGenerator _linkGenerator;
    public DiaryController(
      UserManager<BeonUser> userManager,
      SignInManager<BeonUser> signInManager,
      TopicLogic topicLogic,
      IRepository<Board> boardRepository,
      LinkGenerator linkGenerator,
      ILogger<DiaryController> logger)
    {
      _userManager = userManager;
      _boardRepository = boardRepository;
      _linkGenerator = linkGenerator;
      _signInManager = signInManager;
      _topicLogic = topicLogic;
      _logger = logger;
    }

    [Route("/diary/{userName:required}")]
    [Route("/diary/{userName:required}/{page:int}")]
    public async Task<IActionResult> Show (string userName, int page = 1)
    {
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

      var topics = await _topicLogic.GetTopicPreviewViewModelsAsync(t => t.BoardId.Equals(boardId), page, User);
      if (topics.Count == 0 && page > 1) {
        return NotFound();
      }
      
      int numPages = await _topicLogic.GetNumPagesAsync(t => t.BoardId.Equals(boardId));

      ViewBag.IsDiaryPage = true;
      ViewBag.DiaryTitle = displayName;
      ViewBag.DiarySubtitle = displayName;

      ViewBag.HrBarViewModel = new HrBarViewModel(
        crumbs: new List<LinkViewModel> {new LinkViewModel(displayName, "")},
        pagingInfo: new PagingInfo($"/diary/{userName}", page, numPages));
      
      if (_signInManager.IsSignedIn(User) &&
            userName.Equals(await _userManager.GetUserNameAsync(await _userManager.GetUserAsync(User)))) {
        string? createTopicPath = _linkGenerator.GetPathByAction("Create", "DiaryTopic", new { userName = userName});
        if (createTopicPath == null) {
          return NotFound();
        }
        return View(new DiaryViewModel(new BoardShowViewModel(topics, true, createTopicPath), userName));
      }
      else {
        return View(new DiaryViewModel(new BoardShowViewModel(topics), userName));
      }
    }
  }
}