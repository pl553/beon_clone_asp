namespace Beon.Models.ViewModels {
  public class DiaryTopicShowViewModel {
    public string UserName { get; set; }
    public TopicShowViewModel Topic { get; set; }

    public DiaryTopicShowViewModel(string userName, TopicShowViewModel topic) {
      UserName = userName;
      Topic = topic;
    }
  }
}