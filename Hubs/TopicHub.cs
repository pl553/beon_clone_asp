using Microsoft.AspNetCore.SignalR;
using Beon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Beon.Hubs {
  public class TopicHub : Hub {
    private readonly UserManager<BeonUser> _userManager;
    private IPostRepository _postRepository;
    private ITopicRepository _topicRepository;
    private readonly ILogger<TopicHub> _logger;
    public TopicHub(UserManager<BeonUser> userManager, IPostRepository postRepository, ITopicRepository topicRepository, ILogger<TopicHub> logger) {
      _userManager = userManager;
      _postRepository = postRepository;
      _topicRepository = topicRepository;
      _logger = logger;
    }

    public async Task AddToTopicGroup(int topicId) {
      if (!_topicRepository.TopicWithIdExists(topicId)) {
        return;
      }
      await Groups.AddToGroupAsync(Context.ConnectionId, topicId.ToString());
    }
  }
}