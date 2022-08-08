using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Beon.Controllers
{
  public class BoardController : Controller
  {
    private readonly ILogger _logger;
    private IBoardRepository repository;
    private ITopicRepository topicRepository;
    public BoardController(IBoardRepository repo, ITopicRepository topicRepo, ILogger<BoardController> logger) {
      repository = repo;
      topicRepository = topicRepo;
      _logger = logger;
    }
    public IActionResult Index() => View(new BoardsListViewModel { Boards = repository.Boards });

    [Authorize(Roles = "Admin")]
    public IActionResult Create() => View(new Board());

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Create(Board board) {
      if (ModelState.IsValid) {
        repository.SaveBoard(board);
        return RedirectToAction(nameof(Index));
      }
      else {
        //return View();
        return View();
      }
    }

    public IActionResult Show(int boardId) {
      Board? b = repository.Boards.Where(b => b.BoardId == boardId).Include(b => b.Topics).FirstOrDefault();
      if (b == null) {
        return RedirectToAction(nameof(Index));
      }
      else {
        //_logger.LogCritical($"board id {boardId}");
        return View(new BoardShowViewModel(b));
      }
    }
  }
}