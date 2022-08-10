using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Beon.Controllers
{
  public class TopicController : Controller
  {
    private ITopicRepository repository;
    private IBoardRepository boardRepository;
    private readonly ILogger _logger;
    public TopicController(ITopicRepository repo, IBoardRepository boardRepo, ILogger<TopicController> logger) {
      repository = repo;
      boardRepository = boardRepo;
      _logger = logger;
    }
    public IActionResult Index() {
      return View();
    }

    [HttpPost]
    [Authorize]
    public IActionResult Create(TopicCreateViewModel form) {
      Board? b = boardRepository.Boards.FirstOrDefault(b => b.BoardId == form.boardId);
      //_logger.LogCritical($"board id {form.boardId} {form.Topic.Title}");
      if (ModelState.IsValid && b != default(Board) && form.Topic != null) {
        form.Topic.Board = b;
        repository.SaveTopic(form.Topic);
        return RedirectToAction("Show", "Board", new { boardId = form.boardId });
      }
      else {
        //return View("Show", new { boardId = model.boardId });
        return View("Error");
        //return RedirectToAction("Show", "Board", new { boardId = model.boardId });
      }
    }

    [Route("Topic/{topicId:int}")]
    public IActionResult Show(int topicId) {
      Topic? t = repository.Topics.Where(t => t.TopicId == topicId).Include(t => t.Posts).FirstOrDefault();
      if (t == default(Topic)) {
        return View("Error");
      }
      else {
        return View(new TopicShowViewModel(t));
      }
    }
  }
}