using Microsoft.AspNetCore.Mvc;
using Beon.Models;
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
    private ITopicRepository repository;
    private IBoardRepository boardRepository;
    private IPostRepository postRepository;
    private readonly ILogger _logger;
    public DiaryTopicController(
      ITopicRepository repo,
      IBoardRepository boardRepo,
      IPostRepository postRepo,
      UserManager<BeonUser> userManager,
      ILogger<TopicController> logger) {
      repository = repo;
      boardRepository = boardRepo;
      postRepository = postRepo;
      _userManager = userManager;
      _logger = logger;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(TopicCreateViewModel form) {
      Board? b = boardRepository.Boards.FirstOrDefault(b => b.BoardId == form.boardId);
      //_logger.LogCritical($"board id {form.boardId} {form.Topic.Title}");
      if (ModelState.IsValid && b != default(Board) && form.Topic != null) {
        form.Topic.Board = b;
        repository.SaveTopic(form.Topic);
        form.Op.Topic = form.Topic;
        form.Op.Poster = await _userManager.GetUserAsync(User);
        form.Op.TimeStamp = DateTime.UtcNow;
        postRepository.SavePost(form.Op);
        if (b.Type == BoardType.Diary)
        {
          return RedirectToAction("Show", "Diary", new { userName = b.OwnerName });
        }
        else
        {
          return RedirectToAction("Show", "Board", new { boardId = b.BoardId });
        }
      }
      else {
        //return View("Show", new { boardId = model.boardId });
        return View("Error");
        //return RedirectToAction("Show", "Board", new { boardId = model.boardId });
      }
    }

    [Route("/diary/{userName:required}/{topicId:int}")]
    public IActionResult Show(string userName, int topicId) {
      BeonUser? user = _userManager.Users
        .Where(u => u.UserName.Equals(userName))
        .Include(u => u.Diary)
        .FirstOrDefault();
      
      if (user == null || user.Diary == null)
      {
        return this.RedirectToLocal("");
      }
      Topic? t = repository.Topics
        .Include(t => t.Board)
        .Where(t => t.Board.OwnerName.Equals(userName))
        .Skip(topicId-1)
        .Include(t => t.Posts)
        .ThenInclude(p => p.Poster)
        .FirstOrDefault();

      if (t == default(Topic)) {
        return View("Error");
      }
      else {
        ViewBag.IsDiaryPage = true;
        ViewBag.DiaryTitle = user.DisplayName;
        ViewBag.DiarySubtitle = user.DisplayName;
        return View(new TopicShowViewModel(t));
      }
    }
  }
}