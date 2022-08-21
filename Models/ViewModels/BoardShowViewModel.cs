namespace Beon.Models.ViewModels 
{
  public class BoardShowViewModel {
    //id and timestamp
    public ICollection<Tuple<int,DateTime>> Topics { get; set; }
    public bool CanCreateTopics { get; set; }
    public string CreateTopicPath { get; set; }
    public BoardShowViewModel(ICollection<Tuple<int,DateTime>> topics, bool canCreateTopics = false, string createTopicPath = "") {
      Topics = topics;
      CanCreateTopics = canCreateTopics;
      CreateTopicPath = createTopicPath;
    }
  }
}