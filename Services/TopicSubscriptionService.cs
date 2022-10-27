using Beon.Models;
using Microsoft.EntityFrameworkCore;

namespace Beon.Services {
  public class TopicSubscriptionService {
    private readonly IRepository<TopicSubscription> _topicSubscriptionRepository;
    public TopicSubscriptionService(
      IRepository<TopicSubscription> topicSubscriptionRepository)
    {
      _topicSubscriptionRepository = topicSubscriptionRepository;
    }
    
    public async Task<List<TopicSubscription>> GetWithNewPostsAsync(string userId) {
      return await _topicSubscriptionRepository.Entities
        .Where(ts => ts.SubscriberId!.Equals(userId) && ts.NewPosts)
        .Include(ts => ts.Topic)
        .ToListAsync();
    }

    public async Task<bool> IsUserSubscribedAsync(int topicPostId, string userId) {
      int count = await _topicSubscriptionRepository.Entities
        .Where(s => s.TopicPostId == topicPostId)
        .Where(s => s.SubscriberId == userId)
        .CountAsync();

      return count > 0;
    }
    public async Task SubscribeAsync(int topicPostId, string userId) {
      if (!await IsUserSubscribedAsync(topicPostId, userId))
      {
        await _topicSubscriptionRepository.CreateAsync(
          new TopicSubscription
          {
            TopicPostId = topicPostId,
            SubscriberId = userId
          });
      }
    }

    public async Task SetNewCommentsAsync(int topicPostId) {
      var tss = await _topicSubscriptionRepository.Entities
        .Where(t => t.TopicPostId == topicPostId && t.NewPosts == false)
        .ToListAsync();
      
      foreach (var ts in tss)
      {
        ts.NewPosts = true;
        await _topicSubscriptionRepository.UpdateAsync(ts);
      }
    }

    public async Task UnsetNewCommentsAsync(int topicPostId, string userId) {
      var ts = await _topicSubscriptionRepository.Entities
        .Where(t => t.TopicPostId == topicPostId && t.SubscriberId == userId && t.NewPosts.Equals(true))
        .FirstOrDefaultAsync();

      if (ts != null) {
        ts.NewPosts = false;
        await _topicSubscriptionRepository.UpdateAsync(ts);
      }
    }
  }
}