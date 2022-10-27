using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using Beon.Models;
using Beon.Models.ViewModels;
using Beon.Services;
using Beon.Infrastructure;

namespace Beon.Controllers
{
  using Microsoft.EntityFrameworkCore;

  public class HomeController : Controller
  {
    private readonly IRepository<Topic> _topicRepository;
    private readonly UserManager<BeonUser> _userManager;

    public HomeController(
      IRepository<Topic> topicRepository,
      UserManager<BeonUser> userManager)
    {
      _topicRepository = topicRepository;
      _userManager = userManager;
    }

    [HttpGet]
    [Route("/")]
    [Route("/{page:int}")]
    public async Task<IActionResult> Index(int page = 1)
    { 
      if (page < 1)
      {
        return NotFound();
      }

      var topics = await _topicRepository.Entities
        .TakePage(page)
        .Include(t => t.Poster)
        .ToListAsync();

      if (topics.Count() == 0 && page > 1)
      {
        return NotFound();
      }

      ViewBag.HrBarViewModel = new HrBarViewModel(
        crumbs: new List<LinkViewModel>{new LinkViewModel("BeOn", "")},
        pagingInfo: new PagingInfo("/", page, 8));

      BeonUser? user = await _userManager.GetUserAsync(User);

      return View(new HomePageViewModel(
        await Task.WhenAll(topics.Select(async t => await t.CreateTopicPreviewViewModelAsync(user)))));
    }
  }
}