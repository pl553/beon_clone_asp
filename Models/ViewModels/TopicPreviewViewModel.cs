namespace Beon.Models.ViewModels 
{
  public class TopicPreviewViewModel {
    public int TopicId { get; set; }
    public string Path { get; set; }
    public string Title { get; set; }
    public int PostCount { get; set; }
    public int OpId { get; set; }
    public bool CanEdit { get; set; }
    public TopicPreviewViewModel(int topicId, string path, string title, int opId, int postCount, bool canEdit = false) {
      Path = path;
      Title = title;
      OpId = opId;
      PostCount = postCount;
      CanEdit = canEdit;
      TopicId = topicId;
    }
  }
}