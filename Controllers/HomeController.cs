using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using Beon.Models;
using Beon.Models.ViewModels;

namespace Beon.Controllers
{
  using Microsoft.EntityFrameworkCore;

  public class HomeController : Controller
  {
    private readonly ITopicRepository _topicRepository;
    public HomeController(ITopicRepository topicRepository)
    {
      _topicRepository = topicRepository;
    }

    [HttpGet]
    [Route("/")]
    [Route("/{page:int}")]
    public async Task<IActionResult> Index(int page = 1)
    {
      List<Tuple<int,DateTime>> topics = await _topicRepository.Topics
        .OrderByDescending(t => t.TopicId)
        .Skip((page-1)*Beon.Settings.Page.ItemCount)
        .Take(Beon.Settings.Page.ItemCount)
        .Select(t => new Tuple<int,DateTime>(t.TopicId, t.TimeStamp))
        .ToListAsync();
      
      if (topics.Count() == 0) {
        //return NotFound();
      }

      ViewBag.HrBarViewModel = new HrBarViewModel
        (crumbs: new List<LinkViewModel>{new LinkViewModel("BeOn", "")}, pagingInfo: new PagingInfo("/", page, 8));
      
      return View(new HomeViewModel(new BoardShowViewModel(topics)));
    }
  }
}