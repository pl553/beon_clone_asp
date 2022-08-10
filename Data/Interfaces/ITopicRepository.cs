namespace Beon.Models {
  public interface ITopicRepository {
    IQueryable<Topic> Topics { get; }

    void SaveTopic(Topic topic);
  }
}