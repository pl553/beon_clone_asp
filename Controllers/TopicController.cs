using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Beon.Infrastructure;

namespace Beon.Controllers
{
  public class TopicController : Controller
  {
    private readonly UserManager<BeonUser> _userManager;
    private ITopicRepository repository;
    private IBoardRepository boardRepository;
    private IPostRepository postRepository;
    private readonly ILogger _logger;
    public TopicController(
      ITopicRepository repo,
      IBoardRepository boardRepo,
      IPostRepository postRepo,
      UserManager<BeonUser> userManager,
      ILogger<TopicController> logger) {
      repository = repo;
      boardRepository = boardRepo;
      postRepository = postRepo;
      _userManager = userManager;
      _logger = logger;
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int topicId, string returnUrl) {
      BeonUser? u = await _userManager.GetUserAsync(User);
      Topic? t = await repository.Topics
        .Where(t => t.TopicId.Equals(topicId))
        .Include(t => t.Board)
        .FirstOrDefaultAsync();

      if (u != null && t != null && await repository.UserMayEditTopicAsync(t, u)) {
        repository.DeleteTopic(t);
        return this.RedirectToLocal(returnUrl);
      }

      return NotFound();
    }
  }
}