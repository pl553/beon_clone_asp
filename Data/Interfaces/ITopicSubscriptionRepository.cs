using Microsoft.EntityFrameworkCore;

namespace Beon.Models {
  public interface ITopicSubscriptionRepository {
    IQueryable<TopicSubscription> TopicSubscriptions { get; }

    void SaveTopicSubscription(TopicSubscription TopicSubscription);
    void UpdateTopicSubscription(TopicSubscription topicSubscription);
    async Task<bool> IsUserSubscribedAsync(int topicId, string userId) {
      int count = await TopicSubscriptions
        .Where(s => s.TopicId.Equals(topicId))
        .Where(s => s.SubscriberId!.Equals(userId))
        .CountAsync();

      return count > 0;
    }
    async Task SubscribeAsync(int topicId, string userId) {
      if (!await IsUserSubscribedAsync(topicId, userId)) {
        SaveTopicSubscription(new TopicSubscription { TopicId = topicId, SubscriberId = userId });
      }
    }

    async Task SetNewCommentsAsync(int topicId) {
      var tss = await TopicSubscriptions
        .Where(t => t.TopicId.Equals(topicId) && t.NewPosts.Equals(false))
        .ToListAsync();
      
      foreach (var ts in tss) {
        ts.NewPosts = true;
        UpdateTopicSubscription(ts);
      }
    }

    async Task UnsetNewCommentsAsync(int topicId, string userId) {
      var ts = await TopicSubscriptions
        .Where(t => t.TopicId.Equals(topicId) && t.SubscriberId!.Equals(userId) && t.NewPosts.Equals(true))
        .FirstOrDefaultAsync();

      if (ts != null) {
        ts.NewPosts = false;
        UpdateTopicSubscription(ts);
      }
    }
  }
}