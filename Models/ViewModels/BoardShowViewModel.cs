namespace Beon.Models.ViewModels 
{
  public class BoardShowViewModel {
    public ICollection<int> TopicIds { get; set; }
    public bool CanCreateTopics { get; set; }
    public string CreateTopicPath { get; set; }
    public BoardShowViewModel(ICollection<int> topicIds, bool canCreateTopics = false, string createTopicPath = "") {
      TopicIds = topicIds;
      CanCreateTopics = canCreateTopics;
      CreateTopicPath = createTopicPath;
    }
  }
}