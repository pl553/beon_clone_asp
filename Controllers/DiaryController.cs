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
    private readonly BoardLogic _boardLogic;
    private readonly IRepository<Board> _boardRepository;
    private readonly LinkGenerator _linkGenerator;
    public DiaryController(
      UserManager<BeonUser> userManager,
      SignInManager<BeonUser> signInManager,
      BoardLogic boardLogic,
      IRepository<Board> boardRepository,
      LinkGenerator linkGenerator,
      ILogger<DiaryController> logger)
    {
      _userManager = userManager;
      _boardRepository = boardRepository;
      _linkGenerator = linkGenerator;
      _signInManager = signInManager;
      _boardLogic = boardLogic;
      _logger = logger;
    }

    [Route("/diary/{userName:required}")]
    [Route("/diary/{userName:required}/{page:int}")]
    public async Task<IActionResult> Show (string userName, int page = 1)
    {
      BeonUser? user = await _userManager.Users
        .Where(u => u.UserName.Equals(userName))
        .Include(u => u.Diary)
        .FirstOrDefaultAsync();
      
      if (user == null)
      {
        return NotFound();
      }

      if (user.Diary == null)
      {
        throw new Exception("invalid user: has no diary");
      }

      var topics = await _boardLogic.GetTopicPreviewViewModelsAsync(t => t.BoardId.Equals(user.Diary.BoardId), page, User);
      if (topics.Count == 0 && page > 1) {
        return NotFound();
      }

      int numPages = await _boardLogic.GetNumPagesAsync(t => t.BoardId.Equals(user.Diary.BoardId));

      ViewBag.IsDiaryPage = true;
      ViewBag.DiaryTitle = user.DisplayName;
      ViewBag.DiarySubtitle = user.DisplayName;

      ViewBag.HrBarViewModel = new HrBarViewModel(
        crumbs: new List<LinkViewModel> {new LinkViewModel(user.DisplayName, "")},
        pagingInfo: new PagingInfo($"/diary/{userName}", page, numPages));
      
      if (_signInManager.IsSignedIn(User) &&
            userName.Equals(await _userManager.GetUserNameAsync(await _userManager.GetUserAsync(User)))) {
        string? createTopicPath = _linkGenerator.GetPathByAction("Create", "DiaryTopic", new { userName = userName});
        if (createTopicPath == null) {
          return NotFound();
        }
        return View(new DiaryViewModel(new BoardViewModel(topics, true, createTopicPath), userName));
      }
      else {
        return View(new DiaryViewModel(new BoardViewModel(topics), userName));
      }
    }
  }
}