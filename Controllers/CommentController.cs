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
    private readonly IViewComponentRenderService _vcRender;
    private readonly TopicSubscriptionLogic _topicSubscriptionLogic;
    private IHubContext<TopicHub> _hubContext;    
    private readonly ILogger _logger;
    private readonly PostLogic _postLogic;
    private readonly TopicLogic _topicLogic;
    public CommentController(
      PostLogic postLogic,
      TopicLogic topicLogic,
      IRepository<Comment> repo,
      IRepository<Topic> topicRepository,
      UserManager<BeonUser> userManager,
      IHubContext<TopicHub> hubContext,
      ILogger<CommentController> logger,
      TopicSubscriptionLogic topicSubscriptionLogic,
      IViewComponentRenderService vcRender)
    {
      _postLogic = postLogic;
      _topicLogic = topicLogic;
      _commentRepository = repo;
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
      Topic? t = await _topicRepository.Entities
        .Where(t => t.TopicId.Equals(topicId))
        .FirstOrDefaultAsync();
      
      if (t == null) {
        return NotFound();
      }
      
      BeonUser? u = await _userManager.GetUserAsync(User); 
         
      if (u == null) {
        return NotFound();
      }

      await _topicSubscriptionLogic.SubscribeAsync(topicId, u.Id);
      await _topicSubscriptionLogic.SetNewCommentsAsync(topicId);

      Comment p = new Comment { TopicId = topicId, Body = model.Body, TimeStamp = DateTime.UtcNow, Poster = u };
      await _commentRepository.CreateAsync(p);

      await _hubContext.Clients.Group(topicId.ToString()).SendAsync("ReceiveComment", p.PostId);

      return new JsonResult("OK");
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int postId, string returnUrl) {
      BeonUser? u = await _userManager.GetUserAsync(User);
      Comment? p = await _commentRepository.Entities
        .Where(p => p.PostId.Equals(postId))
        .Include(p => p.Topic)
        .FirstOrDefaultAsync();

      if (u != null && p != null && await _postLogic.UserMayDeleteCommentAsync(p, u)) {
        await _commentRepository.DeleteAsync(p);
        return this.RedirectToLocal(returnUrl);
      }

      return NotFound();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetRawHtml(int postId)
    {
      BeonUser? u = await _userManager.GetUserAsync(User);
      Comment? p = await _commentRepository.Entities
        .Where(p => p.PostId.Equals(postId))
        .Include(p => p.Topic)
        .FirstOrDefaultAsync();

      if (p == null || p.Topic == null)
      {
        return NotFound();
      }

      CommentViewModel vm = await _postLogic.GetCommentViewModelAsync(p, u);

      string postRawHtml = await _vcRender.RenderAsync(ControllerContext, ViewData, TempData, "Comment", new { comment = vm });

      return Content(postRawHtml, "text/html");
    }
  }
}