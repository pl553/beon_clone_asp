using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;
using Beon.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Beon.Infrastructure;

namespace Beon.Controllers
{
  public class TopicController : Controller
  {
    private readonly UserManager<BeonUser> _userManager;
    private readonly IRepository<Topic> _topicRepository;
    private readonly TopicLogic _topicLogic;
    private IRepository<Board> boardRepository;
    private readonly ILogger _logger;
    public TopicController(
      IRepository<Topic> topicRepository,
      TopicLogic topicLogic,
      IRepository<Board> boardRepo,
      UserManager<BeonUser> userManager,
      ILogger<TopicController> logger)
    {
      _topicRepository = topicRepository;
      boardRepository = boardRepo;
      _topicLogic = topicLogic;
      _userManager = userManager;
      _logger = logger;
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int topicId, string returnUrl) {
      BeonUser? u = await _userManager.GetUserAsync(User);
      Topic? t = await _topicRepository.Entities
        .Where(t => t.TopicId.Equals(topicId))
        .Include(t => t.Board)
        .FirstOrDefaultAsync();

      if (u != null && t != null && await _topicLogic.UserMayEditTopicAsync(t, u)) {
        await _topicRepository.DeleteAsync(t);
        return this.RedirectToLocal(returnUrl);
      }

      return NotFound();
    }
  }
}