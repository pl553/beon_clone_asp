using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Beon.Components {
  public class TopicPreviewViewComponent : ViewComponent
  {
    private readonly ITopicRepository _topicRepository;
    private readonly IPostRepository _postRepository;
    private readonly LinkGenerator _linkGenerator;

    public TopicPreviewViewComponent(
        ITopicRepository topicRepository, 
        IPostRepository postRepository,
        LinkGenerator linkGenerator) {
      _topicRepository = topicRepository;
      _postRepository = postRepository;
      _linkGenerator = linkGenerator;
    }

    public async Task<IViewComponentResult> InvokeAsync(int topicId) {
      Topic? t = await _topicRepository.Topics
        .Where(t => t.TopicId.Equals(topicId))
        .Include(t => t.Board)
        .FirstOrDefaultAsync();

      if (t == null || t.Board == null) {
        throw new Exception("Invalid topicId");
      }

      string topicPath = await _topicRepository.GetTopicPathAsync(t);

      int opId = await _postRepository.Posts
        .Where(p => p.TopicId.Equals(topicId))
        .Select(p => p.PostId)
        .FirstOrDefaultAsync(); 

      int count = await _postRepository.Posts
        .Where(p => p.TopicId.Equals(topicId))
        .CountAsync();
      
      if (opId == 0) {
        throw new Exception("Invalid topic");
      }

      return View(new TopicPreviewViewModel(topicPath, t.Title, opId, count));
    }
  }
}