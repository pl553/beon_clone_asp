namespace Beon.Models.ViewModels 
{
  public class BoardShowViewModel {
    public ICollection<TopicPreviewViewModel> Topics { get; set; }
    public bool CanCreateTopics { get; set; }
    public string CreateTopicPath { get; set; }
    public BoardShowViewModel(ICollection<TopicPreviewViewModel> topics, bool canCreateTopics = false, string createTopicPath = "") {
      Topics = topics;
      CanCreateTopics = canCreateTopics;
      CreateTopicPath = createTopicPath;
    }
  }
}