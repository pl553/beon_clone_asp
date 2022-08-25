using Microsoft.EntityFrameworkCore;

namespace Beon.Models {
  public class EFTopicSubscriptionRepository : ITopicSubscriptionRepository {
    private BeonDbContext context;
    public EFTopicSubscriptionRepository(BeonDbContext ctx) {
      context = ctx;
    }

    public void SaveTopicSubscription(TopicSubscription topicSubscription) {
      context.TopicSubscriptions.Add(topicSubscription);
      context.SaveChanges();
    }

    public void UpdateTopicSubscription(TopicSubscription topicSubscription) {
      context.Entry(topicSubscription).State = EntityState.Modified;
      context.SaveChanges();
    }
    public IQueryable<TopicSubscription> TopicSubscriptions => context.TopicSubscriptions;
  }
}