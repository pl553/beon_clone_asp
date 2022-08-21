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
    public async Task<IActionResult> Index()
    {
      List<int> topicIds = await _topicRepository.Topics
        .OrderByDescending(t => t.TopicId)
        .Take(100)
        .Select(t => t.TopicId)
        .ToListAsync();
      
      return View(new HomeViewModel(new BoardShowViewModel(topicIds)));
    }
  }
}