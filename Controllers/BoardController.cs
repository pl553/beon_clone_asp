using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;

namespace Beon.Controllers
{
  public class BoardController : Controller
  {
    private IBoardRepository repository;
    public BoardController(IBoardRepository repo) {
      repository = repo;
    }
    public IActionResult Index() => View(new BoardsListViewModel { Boards = repository.Boards });

    public IActionResult Create() => View(new Board());

    [HttpPost]
    public IActionResult Create(Board board) {
      if (ModelState.IsValid) {
        repository.SaveBoard(board);
        return RedirectToAction(nameof(Index));
      }
      else {
        return View();
      }
    }
  }
}