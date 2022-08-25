namespace Beon.Models {
  public class EFTopicRepository : ITopicRepository {
    private BeonDbContext context;
    private LinkGenerator _linkGenerator;
    public EFTopicRepository(BeonDbContext ctx, LinkGenerator linkGenerator) {
      context = ctx;
      _linkGenerator = linkGenerator;
    }

    public void SaveTopic(Topic topic) {
      context.Topics.Add(topic);
      context.SaveChanges();
    }

    public async Task<string> GetTopicPathAsync(Topic topic) {
      if (topic.Board == null) {
        await context.Entry(topic)
          .Reference(t => t.Board)
          .LoadAsync();
      }
      if (topic.Board == null) {
        throw new Exception("invalid topic");
      }
      string? res = null;
      if (topic.Board.Type == BoardType.Diary) {
        res = _linkGenerator.GetPathByAction("Show", "DiaryTopic", new { userName = topic.Board.OwnerName, topicOrd = topic.TopicOrd });
      }
      else throw new Exception("unsupported boardtype");
      if (res == null) throw new Exception("could'nt generate topic path");
      return res;
    }
    public IQueryable<Topic> Topics => context.Topics;
  }
}