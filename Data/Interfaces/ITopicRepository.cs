using Microsoft.EntityFrameworkCore;

namespace Beon.Models {
  public interface ITopicRepository {
    IQueryable<Topic> Topics { get; }

    void SaveTopic(Topic topic);

    bool TopicWithIdExists(int topicId) =>
      Topics.Where(t => t.TopicId.Equals(topicId)).Count() > 0;

    Task<string> GetTopicPathAsync(Topic topic);
  }
}