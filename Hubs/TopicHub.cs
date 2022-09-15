using Microsoft.AspNetCore.SignalR;
using Beon.Models;
using Beon.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Beon.Hubs {
  public class TopicHub : Hub {
    private readonly UserManager<BeonUser> _userManager;
    private TopicLogic _topicLogic;
    private readonly TopicSubscriptionLogic _topicSubscriptionLogic;
    private readonly ILogger<TopicHub> _logger;
    public TopicHub(
        UserManager<BeonUser> userManager,
        TopicLogic topicLogic,
        ILogger<TopicHub> logger,
        TopicSubscriptionLogic topicSubscriptionLogic) {
      _userManager = userManager;
      _topicLogic = topicLogic;
      _logger = logger;
      _topicSubscriptionLogic = topicSubscriptionLogic;
    }

    public async Task AddToTopicGroup(int topicId) {
      if (!await _topicLogic.TopicWithIdExistsAsync(topicId)) {
        return;
      }
      await Groups.AddToGroupAsync(Context.ConnectionId, topicId.ToString());
    }

    public async Task ReceivedComment(int topicId) {
        if (!await _topicLogic.TopicWithIdExistsAsync(topicId)) {
          return;
        }
        BeonUser? user = await _userManager.GetUserAsync(Context.User);
        if (user != null) {
          await _topicSubscriptionLogic.UnsetNewCommentsAsync(topicId, user.Id);
        }
    }
  }
}