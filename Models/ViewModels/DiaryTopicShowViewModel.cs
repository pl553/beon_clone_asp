namespace Beon.Models.ViewModels {
  public class DiaryTopicViewModel {
    public string UserName { get; set; }
    public TopicViewModel Topic { get; set; }

    public DiaryTopicViewModel(string userName, TopicViewModel topic) {
      UserName = userName;
      Topic = topic;
    }
  }
}