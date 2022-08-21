/*using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Beon.Components {
  public class TopicViewComponent : ViewComponent
  {
    private readonly ITopicRepository _topicRepository;
    private readonly IPostRepository _postRepository;
    private readonly LinkGenerator _linkGenerator;

    public TopicViewComponent(
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

      string? postCreatePath = null;
      if (t.Board.Type == BoardType.Diary) {
        postCreatePath = _linkGenerator.GetPathByAction("Create", "DiaryPost", new { userName = t.Board.OwnerName, topicOrd = t.TopicOrd });  
      }

      if (postCreatePath == null) {
        throw new Exception("Couldn't generate post create path");
      }

      ICollection<int> postIds = await _postRepository.Posts
        .Where(p => p.TopicId.Equals(topicId))
        .Select(p => p.PostId)
        .ToListAsync(); 

      if (postIds.Count() == 0) {
        throw new Exception("Invalid topic");
      }

      return View(new TopicShowViewModel(postCreatePath, t.Title, postIds));
    }
  }
}*/