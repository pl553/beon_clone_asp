using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Beon.Controllers
{
  public class DiaryController : Controller
  {
    private readonly ILogger _logger;
    private readonly UserManager<BeonUser> _userManager;
    private readonly SignInManager<BeonUser> _signInManager;
    private readonly IRepository<Topic> _topicRepository;
    private readonly IRepository<Board> _boardRepository;
    private readonly LinkGenerator _linkGenerator;
    public DiaryController(
      UserManager<BeonUser> userManager,
      SignInManager<BeonUser> signInManager,
      IRepository<Topic> topicRepository,
      IRepository<Board> boardRepository,
      LinkGenerator linkGenerator,
      ILogger<DiaryController> logger) {
      _userManager = userManager;
      _topicRepository = topicRepository;
      _boardRepository = boardRepository;
      _linkGenerator = linkGenerator;
      _signInManager = signInManager;
      _logger = logger;
    }

    [Route("/diary/{userName:required}")]
    public async Task<IActionResult> Show (string userName)
    {
      string? displayName = await _userManager.Users
        .Where(u => u.UserName.Equals(userName))
        .Select(u => u.DisplayName)
        .FirstOrDefaultAsync();
      
      if (displayName == null) {
        return NotFound();
      }
      
      Board? b = await _boardRepository.Entities
        .Where(b => b.OwnerName.Equals(userName) && b.Type.Equals(BoardType.Diary))
        .Include(b => b.Topics)
        .FirstOrDefaultAsync();

      if (b == null) {
        return NotFound();
      }

      ICollection<Tuple<int,DateTime>> topics = await _topicRepository.Entities
        .Where(t => t.BoardId.Equals(b.BoardId))
        .OrderByDescending(t => t.TopicId)
        .Select(t => new Tuple<int,DateTime>(t.TopicId, t.TimeStamp))
        .ToListAsync();

      ViewBag.IsDiaryPage = true;
      ViewBag.DiaryTitle = displayName;
      ViewBag.DiarySubtitle = displayName;

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