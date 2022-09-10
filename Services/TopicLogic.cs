using Beon.Models;
using Microsoft.EntityFrameworkCore;

namespace Beon.Services {
  public class TopicLogic {
    private readonly IRepository<Post> _postRepository;
    private readonly IRepository<Topic> _topicRepository;
    private readonly IRepository<Board> _boardRepository;
    private readonly LinkGenerator _linkGenerator;
    public TopicLogic(
      IRepository<Post> postRepository,
      IRepository<Topic> topicRepository,
      IRepository<Board> boardRepository,
      LinkGenerator linkGenerator)
    {
      _postRepository = postRepository;
      _boardRepository = boardRepository;
      _topicRepository = topicRepository;
      _linkGenerator = linkGenerator;
    }
    public async Task<bool> TopicWithIdExistsAsync(int topicId) =>
      await _topicRepository.Entities.Where(t => t.TopicId.Equals(topicId)).CountAsync() > 0;
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

    public async Task<ICollection<int>> GetPostIdsOfTopicAsync(int topicId) {
      return await _postRepository.Entities
        .Where(p => p.TopicId.Equals(topicId))
        .Select(p => p.PostId)
        .ToListAsync();
    }
    private async Task<Board> LoadBoardAsync(Topic topic) {
      if (topic.Board == null) {
        topic.Board = await _boardRepository.Entities
          .Where(b => b.BoardId.Equals(topic.BoardId))
          .FirstOrDefaultAsync();
      }
      if (topic.Board == null) {
        throw new Exception("invalid topic");
      }
      return topic.Board;
    }
  }
}