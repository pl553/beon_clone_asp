using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using Beon.Models;
using Beon.Models.ViewModels;
using Beon.Services;

namespace Beon.Controllers
{
  using Microsoft.EntityFrameworkCore;

  public class HomeController : Controller
  {
    private readonly BoardLogic _boardLogic;
    public HomeController(BoardLogic boardLogic)
    {
      _boardLogic = boardLogic;
    }

    [HttpGet]
    [Route("/")]
    [Route("/{page:int}")]
    public async Task<IActionResult> Index(int page = 1)
    { 
      var topics = await _boardLogic.GetTopicPreviewViewModelsAsync(t => true, page, User);

      if (topics.Count() == 0 && page > 1) {
        return NotFound();
      }

      ViewBag.HrBarViewModel = new HrBarViewModel
        (crumbs: new List<LinkViewModel>{new LinkViewModel("BeOn", "")}, pagingInfo: new PagingInfo("/", page, 8));
      
      return View(new HomeViewModel(new BoardViewModel(topics)));
    }
  }
}