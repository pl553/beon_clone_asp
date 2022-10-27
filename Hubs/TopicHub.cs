using Microsoft.AspNetCore.SignalR;
using Beon.Models;
using Beon.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Beon.Hubs {
  public class TopicHub : Hub {
    private readonly UserManager<BeonUser> _userManager;
    private readonly IRepository<Topic> _topicRepository;
    private readonly TopicService _topicService;
    private readonly TopicSubscriptionService _topicSubscriptionService;
    private readonly ILogger<TopicHub> _logger;
    public TopicHub(
      UserManager<BeonUser> userManager,
      IRepository<Topic> topicRepository,
      TopicService topicService,
      ILogger<TopicHub> logger,
      TopicSubscriptionService topicSubscriptionService)
    {
      _userManager = userManager;
      _topicRepository = topicRepository;
      _topicService = topicService;
      _logger = logger;
      _topicSubscriptionService = topicSubscriptionService;
    }

    public async Task AddToTopicGroup(int topicPostId)
    {
      Topic? t = await _topicRepository.Entities
        .Where(t => t.PostId == topicPostId)
        .FirstOrDefaultAsync();

      if (t == null)
      {
        return;
      }

      BeonUser? user = await _userManager.GetUserAsync(Context.User);

      if (!await t.UserCanReadAsync(user))
      {
        return;
      }

      await Groups.AddToGroupAsync(Context.ConnectionId, topicPostId.ToString());
    }

    public async Task ReceivedComment(int topicPostId)
    {
      if (!await _topicService.TopicExistsAsync(topicPostId))
      {
        return;
      }
      BeonUser? user = await _userManager.GetUserAsync(Context.User);
      if (user != null) {
        await _topicSubscriptionService.UnsetNewCommentsAsync(topicPostId, user.Id);
      }
    }
  }
}