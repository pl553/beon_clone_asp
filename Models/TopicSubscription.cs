using Microsoft.EntityFrameworkCore;

namespace Beon.Models {
  public class TopicSubscription {
    public int TopicSubscriptionId { get; set; }
    public string? SubscriberId { get; set; }
    public BeonUser? Subscriber { get; set; }
    public int TopicId { get; set; }
    public Topic? Topic { get; set; }
    public bool NewPosts { get; set; } = false;
  }
}