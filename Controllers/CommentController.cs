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
  public class CommentController : Controller
  {
    private IRepository<Comment> _commentRepository;
    private IRepository<Topic> _topicRepository;
    private readonly UserManager<BeonUser> _userManager;
    private readonly TopicSubscriptionService _topicSubscriptionService;
    private IHubContext<TopicHub> _hubContext;    
    private readonly ILogger _logger;

    public CommentController(
      IRepository<Comment> repo,
      IRepository<Topic> topicRepository,
      UserManager<BeonUser> userManager,
      IHubContext<TopicHub> hubContext,
      ILogger<CommentController> logger,
      TopicSubscriptionService topicSubscriptionService)
    {
      _commentRepository = repo;
      _topicRepository = topicRepository;
      _userManager = userManager;
      _hubContext = hubContext;
      _logger = logger;
      _topicSubscriptionService = topicSubscriptionService;
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CommentFormModel model)
    {
      if (!ModelState.IsValid)
      {
        return NotFound();
      }

      var t = await _topicRepository.Entities
        .Where(t => t.PostId == model.TopicPostId)
        .FirstOrDefaultAsync();
      
      if (t == null) {
        return NotFound();
      }
      
      BeonUser? u = await _userManager.GetUserAsync(User); 
         
      if (u == null || !await t.UserCanCommentAsync(u))
      {
        return NotFound();
      }

      await _topicSubscriptionService.SubscribeAsync(model.TopicPostId, u.Id);
      await _topicSubscriptionService.SetNewCommentsAsync(model.TopicPostId);

      Comment p = new Comment
      {
        TopicPostId = model.TopicPostId,
        Body = model.Body,
        TimeStamp = DateTime.UtcNow,
        Poster = u
      };

      await _commentRepository.CreateAsync(p);

      await _hubContext.Clients.Group(model.TopicPostId.ToString())
        .SendAsync("ReceiveComment", p.PostId);

      return this.RedirectToLocal(await t.GetPathAsync());
    }
  }
}