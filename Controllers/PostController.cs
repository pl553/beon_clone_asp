using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Hubs;
using Beon.Models.ViewModels;
using Beon.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Beon.Infrastructure;

namespace Beon.Controllers
{
  public class PostController : Controller
  {
    private IRepository<Post> _postRepository;
    private IRepository<Topic> _topicRepository;
    private readonly UserManager<BeonUser> _userManager;
    private readonly IViewComponentRenderService _vcRender;
    private readonly TopicSubscriptionLogic _topicSubscriptionLogic;
    private IHubContext<TopicHub> _hubContext;    
    private readonly ILogger _logger;
    public PostController(
      IRepository<Post> repo,
      IRepository<Topic> topicRepository,
      UserManager<BeonUser> userManager,
      IHubContext<TopicHub> hubContext,
      ILogger<PostController> logger,
      TopicSubscriptionLogic topicSubscriptionLogic,
      IViewComponentRenderService vcRender) {
      _postRepository = repo;
      _topicRepository = topicRepository;
      _userManager = userManager;
      _hubContext = hubContext;
      _logger = logger;
      _vcRender = vcRender;
      _topicSubscriptionLogic = topicSubscriptionLogic;
    }
    public IActionResult Index() {
      return View();
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int topicId, PostFormModel model) {
      topicId = await _topicRepository.Entities
        .Where(t => t.TopicId.Equals(topicId))
        .Select(t => t.TopicId)
        .FirstOrDefaultAsync();
      
      if (topicId == 0) {
        return NotFound();
      }
      
      BeonUser? u = await _userManager.GetUserAsync(User); 
         
      if (u == null) {
        return NotFound();
      }

      await _topicSubscriptionLogic.SubscribeAsync(topicId, u.Id);
      await _topicSubscriptionLogic.SetNewCommentsAsync(topicId);

      Post p = new Post { TopicId = topicId, Body = model.Body, TimeStamp = DateTime.UtcNow, Poster = u };
      await _postRepository.CreateAsync(p);

      string postRawHtml = await _vcRender.RenderAsync(ControllerContext, ViewData, TempData, "Post", new { postId = p.PostId, showDate = true });

      await _hubContext.Clients.Group(topicId.ToString()).SendAsync("ReceivePost", postRawHtml);

      return new JsonResult("OK");
    }
  }
}