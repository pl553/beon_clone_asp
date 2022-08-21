using Microsoft.EntityFrameworkCore;

namespace Beon.Models {
  public interface IPostRepository {
    IQueryable<Post> Posts { get; }

    void SavePost(Post Post);

    public Task<List<int>> GetPostIdsOfTopicAsync(int topicId) {
      return Posts.Where(p => p.TopicId.Equals(topicId)).Select(p => p.PostId).ToListAsync();
    }
  }
}