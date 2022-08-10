namespace Beon.Models {
  public class EFTopicRepository : ITopicRepository {
    private BeonDbContext context;
    public EFTopicRepository(BeonDbContext ctx) {
      context = ctx;
    }

    public void SaveTopic(Topic topic) {
      context.Topics.Add(topic);
      context.SaveChanges();
    }
    public IQueryable<Topic> Topics => context.Topics;
  }
}