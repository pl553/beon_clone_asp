using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;

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
    public IActionResult Create(TopicFormViewModel topicForm) {
      Board? b = boardRepository.Boards.FirstOrDefault(b => b.BoardId == topicForm.boardId);
      //_logger.LogCritical($"board id {topicForm.boardId} {topicForm.Topic.Title}");
      if (ModelState.IsValid && b != null && topicForm.Topic != null) {
        topicForm.Topic.Board = b;
        repository.SaveTopic(topicForm.Topic);
        return RedirectToAction("Show", "Board", new { boardId = topicForm.boardId });
      }
      else {
        //return View("Show", new { boardId = topicForm.boardId });
        return RedirectToAction("Show", "Board", new { boardId = topicForm.boardId });
      }
    }
  }
}