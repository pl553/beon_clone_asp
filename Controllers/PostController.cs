using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Beon.Controllers
{
  public class PostController : Controller
  {
    private IPostRepository repository;
    private ITopicRepository TopicRepository;
    private readonly ILogger _logger;
    public PostController(IPostRepository repo, ITopicRepository TopicRepo, ILogger<PostController> logger) {
      repository = repo;
      TopicRepository = TopicRepo;
      _logger = logger;
    }
    public IActionResult Index() {
      return View();
    }

    [HttpPost]
    [Authorize]
    public IActionResult Create(PostCreateViewModel model) {
      Topic? b = TopicRepository.Topics.FirstOrDefault(b => b.TopicId == model.TopicId);
      //_logger.LogCritical($"Topic id {model.TopicId} {model.Post.Title}");
      if (ModelState.IsValid && b != default(Topic) && model.Post != null) {
        model.Post.Topic = b;
        model.Post.TimeStamp = DateTime.UtcNow;
        repository.SavePost(model.Post);
        return RedirectToAction("Show", "Topic", new { TopicId = model.TopicId });
      }
      else {
        //return View("Show", new { TopicId = model.TopicId });
        return View("Error");
        //return RedirectToAction("Show", "Topic", new { TopicId = model.TopicId });
      }
    }
  }
}