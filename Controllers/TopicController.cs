using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Beon.Infrastructure;

namespace Beon.Controllers
{
  public class TopicController : Controller
  {
    private readonly UserManager<BeonUser> _userManager;
    private ITopicRepository repository;
    private IBoardRepository boardRepository;
    private IPostRepository postRepository;
    private readonly ILogger _logger;
    public TopicController(
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
    public IActionResult Index() {
      return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(TopicCreateViewModel form, string? returnUrl) {
      Board? b = boardRepository.Boards.FirstOrDefault(b => b.BoardId == form.boardId);
      //_logger.LogCritical($"board id {form.boardId} {form.Topic.Title}");
      if (ModelState.IsValid && b != default(Board) && form.Topic != null) {
        form.Topic.Board = b;
        repository.SaveTopic(form.Topic);
        form.Op.Topic = form.Topic;
        form.Op.Poster = await _userManager.GetUserAsync(User);
        form.Op.TimeStamp = DateTime.UtcNow;
        postRepository.SavePost(form.Op);
        if (returnUrl == null)
        {
          return RedirectToAction("Show", "Board", new { boardId = form.boardId });
        }
        else
        {
          return this.RedirectToLocal(returnUrl);
        }
      }
      else {
        //return View("Show", new { boardId = model.boardId });
        return View("Error");
        //return RedirectToAction("Show", "Board", new { boardId = model.boardId });
      }
    }

    [Route("Topic/{topicId:int}")]
    public IActionResult Show(int topicId) {
      Topic? t = repository.Topics.Where(t => t.TopicId == topicId).Include(t => t.Posts).ThenInclude(p => p.Poster).FirstOrDefault();
      if (t == default(Topic)) {
        return View("Error");
      }
      else {
        return View(new TopicShowViewModel(t));
      }
    }
  }
}