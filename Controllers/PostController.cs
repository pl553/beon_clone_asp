using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Hubs;
using Beon.Models.ViewModels;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Beon.Infrastructure;

namespace Beon.Controllers
{
  public class PostController : Controller
  {
    private IPostRepository _postRepository;
    private ITopicRepository _topicRepository;
    private readonly UserManager<BeonUser> _userManager;
    private readonly IViewComponentRenderService _vcRender;
    private readonly ITopicSubscriptionRepository _tsRepository;
    private IHubContext<TopicHub> _hubContext;    
    private readonly ILogger _logger;
    public PostController(
      IPostRepository repo,
      ITopicRepository TopicRepo,
      UserManager<BeonUser> userManager,
      IHubContext<TopicHub> hubContext,
      ILogger<PostController> logger,
      ITopicSubscriptionRepository tsRepository,
      IViewComponentRenderService vcRender) {
      _postRepository = repo;
      _topicRepository = TopicRepo;
      _userManager = userManager;
      _hubContext = hubContext;
      _logger = logger;
      _vcRender = vcRender;
      _tsRepository = tsRepository;
    }
    public IActionResult Index() {
      return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(int topicId, PostFormModel model) {
      topicId = await _topicRepository.Topics
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

      _tsRepository.SubscribeAsync(topicId, u.Id);
      _tsRepository.SetNewCommentsAsync(topicId);

      Post p = new Post { TopicId = topicId, Body = model.Body, TimeStamp = DateTime.UtcNow, Poster = u };
      _postRepository.SavePost(p);

      string postRawHtml = await _vcRender.RenderAsync(ControllerContext, ViewData, TempData, "Post", new { postId = p.PostId, showDate = true });

      await _hubContext.Clients.Group(topicId.ToString()).SendAsync("ReceivePost", postRawHtml);

      return new JsonResult("OK");
    }
  }
}