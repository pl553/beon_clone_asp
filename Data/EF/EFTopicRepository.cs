using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

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

    public void DeleteTopic(Topic topic) {
      context.Entry(topic).State = EntityState.Deleted;
      context.SaveChanges();
    }

    public async Task<string> GetTopicPathAsync(Topic topic) {
      Board board = await LoadBoardAsync(topic);
      string? res = null;
      if (board.Type == BoardType.Diary) {
        res = _linkGenerator.GetPathByAction("Show", "DiaryTopic", new { userName = board.OwnerName, topicOrd = topic.TopicOrd });
      }
      else throw new Exception("unsupported boardtype");
      if (res == null) throw new Exception("could'nt generate topic path");
      return res;
    }

    public async Task<bool> UserMayEditTopicAsync(Topic topic, BeonUser user) {
      Board board = await LoadBoardAsync(topic);
      if (board.Type == BoardType.Diary) {
        return user.UserName.Equals(board.OwnerName);
      }
      else throw new Exception("unsupported boardtype");
    }

    private async Task<Board> LoadBoardAsync(Topic topic) {
      if (topic.Board == null) {
        await context.Entry(topic)
          .Reference(t => t.Board)
          .LoadAsync();
      }
      if (topic.Board == null) {
        throw new Exception("invalid topic");
      }
      return topic.Board;
    }
    public IQueryable<Topic> Topics => context.Topics;
  }
}