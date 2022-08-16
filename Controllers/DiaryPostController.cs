using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Beon.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Beon.Controllers
{
  public class DiaryPostController : Controller
  {
    private IPostRepository repository;
    private ITopicRepository _topicRepository;
    private readonly UserManager<BeonUser> _userManager;    
    private readonly ILogger _logger;
    public DiaryPostController(
      IPostRepository repo,
      ITopicRepository TopicRepo,
      UserManager<BeonUser> userManager,
      ILogger<DiaryPostController> logger) {
      repository = repo;
      _topicRepository = TopicRepo;
      _userManager = userManager;
      _logger = logger;
    }
    public IActionResult Index() {
      return View();
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    [Route("/diary/{userName:required}/{topicId:int}/CreatePost")]
    public async Task<IActionResult> Create(string userName, int topicId, PostCreateViewModel model) {
      if (!ModelState.IsValid) {
        return this.RedirectToLocal("");
      }

      Topic? t = _topicRepository.Topics
        .Include(t => t.Board)
        .Where(t => t.Board!.OwnerName.Equals(userName))
        .Where(t => t.Board!.Type.Equals(BoardType.Diary))
        .Skip(topicId-1)
        .FirstOrDefault();

      if (t == default(Topic)) {
        return View("Error");
      }
      //_logger.LogCritical($"Topic id {model.TopicId} {model.Post.Title}");
      if (ModelState.IsValid && t != default(Topic) && model.Post != null) {
        model.Post.Topic = t;
        model.Post.TimeStamp = DateTime.UtcNow;
        model.Post.Poster = await _userManager.GetUserAsync(User);
        repository.SavePost(model.Post);
        return RedirectToAction("Show", "DiaryTopic", new { topicId = topicId, userName = userName });
      }
      else {
        //return View("Show", new { TopicId = model.TopicId });
        return View("Error");
        //return RedirectToAction("Show", "Topic", new { TopicId = model.TopicId });
      }
    }
  }
}