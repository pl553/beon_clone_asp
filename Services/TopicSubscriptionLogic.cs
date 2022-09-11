using Beon.Models;
using Microsoft.EntityFrameworkCore;

namespace Beon.Services {
  public class TopicSubscriptionLogic {
    private readonly IRepository<TopicSubscription> _topicSubscriptionRepository;
    public TopicSubscriptionLogic(
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

    public async Task<bool> IsUserSubscribedAsync(int topicId, string userId) {
      int count = await _topicSubscriptionRepository.Entities
        .Where(s => s.TopicId.Equals(topicId))
        .Where(s => s.SubscriberId!.Equals(userId))
        .CountAsync();

      return count > 0;
    }
    public async Task SubscribeAsync(int topicId, string userId) {
      if (!await IsUserSubscribedAsync(topicId, userId)) {
        await _topicSubscriptionRepository.CreateAsync(new TopicSubscription { TopicId = topicId, SubscriberId = userId });
      }
    }

    public async Task SetNewCommentsAsync(int topicId) {
      var tss = await _topicSubscriptionRepository.Entities
        .Where(t => t.TopicId.Equals(topicId) && t.NewPosts.Equals(false))
        .ToListAsync();
      
      foreach (var ts in tss) {
        ts.NewPosts = true;
        await _topicSubscriptionRepository.UpdateAsync(ts);
      }
    }

    public async Task UnsetNewCommentsAsync(int topicId, string userId) {
      var ts = await _topicSubscriptionRepository.Entities
        .Where(t => t.TopicId.Equals(topicId) && t.SubscriberId!.Equals(userId) && t.NewPosts.Equals(true))
        .FirstOrDefaultAsync();

      if (ts != null) {
        ts.NewPosts = false;
        await _topicSubscriptionRepository.UpdateAsync(ts);
      }
    }
  }
}