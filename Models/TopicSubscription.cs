using Microsoft.EntityFrameworkCore;

namespace Beon.Models {
  public class TopicSubscription {
    public int TopicSubscriptionId { get; set; }

    public string? SubscriberId { get; set; }

    public BeonUser? Subscriber { get; set; }

    public int TopicPostId { get; set; }

    public Topic? Topic { get; set; }

    public bool NewPosts { get; set; } = false;

    public TopicSubscription() { }

    private readonly BeonDbContext _context;

    private TopicSubscription(BeonDbContext context)
    {
      _context = context;
    }

    public async Task<Topic> GetTopicAsync()
    {
      await _context.Entry(this).Reference(ts => ts.Topic).LoadAsync();
      return Topic ?? throw new Exception("invalid ts: no topic");
    }
  }
}