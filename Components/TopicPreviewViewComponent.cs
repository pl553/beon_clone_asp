using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;
using Beon.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Beon.Components {
  public class TopicPreviewViewComponent : ViewComponent
  {
    private readonly IRepository<Topic> _topicRepository;
    private readonly TopicLogic _topicLogic;
    private readonly IRepository<Post> _postRepository;
    private readonly LinkGenerator _linkGenerator;
    private readonly UserManager<BeonUser> _userManager;

    public TopicPreviewViewComponent(
      IRepository<Topic> topicRepository, 
      TopicLogic topicLogic,
      IRepository<Post> postRepository,
      LinkGenerator linkGenerator,
      UserManager<BeonUser> userManager)
    {
      _topicRepository = topicRepository;
      _topicLogic = topicLogic;
      _postRepository = postRepository;
      _linkGenerator = linkGenerator;
      _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync(int topicId) {
      Topic? t = await _topicRepository.Entities
        .Where(t => t.TopicId.Equals(topicId))
        .Include(t => t.Board)
        .FirstOrDefaultAsync();

      if (t == null || t.Board == null) {
        throw new Exception("Invalid topicId");
      }

      string topicPath = await _topicLogic.GetTopicPathAsync(t);

      int opId = await _postRepository.Entities
        .Where(p => p.TopicId.Equals(topicId))
        .Select(p => p.PostId)
        .FirstOrDefaultAsync(); 

      int count = await _postRepository.Entities
        .Where(p => p.TopicId.Equals(topicId))
        .CountAsync();
      
      if (opId == 0) {
        throw new Exception("Invalid topic");
      }

      BeonUser? u = await _userManager.GetUserAsync(UserClaimsPrincipal);
      bool canEdit = false;
      if (u != null) canEdit = await _topicLogic.UserMayEditTopicAsync(t, u);

      return View(new TopicPreviewViewModel(topicId, topicPath, t.Title, opId, count, canEdit));
    }
  }
}