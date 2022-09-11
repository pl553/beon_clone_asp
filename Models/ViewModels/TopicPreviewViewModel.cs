namespace Beon.Models.ViewModels 
{
  public class TopicPreviewViewModel {
    public DateTime TimeStamp { get; set; }
    public int TopicId { get; set; }
    public string Path { get; set; }
    public string Title { get; set; }
    public int PostCount { get; set; }
    public PostViewModel Op { get; set; }
    public bool CanEdit { get; set; }
    public TopicPreviewViewModel(
      int topicId,
      string path,
      string title,
      PostViewModel op,
      int postCount,
      DateTime timeStamp,
      bool canEdit = false)
    {
      Path = path;
      Title = title;
      Op = op;
      PostCount = postCount;
      CanEdit = canEdit;
      TimeStamp = timeStamp;
      TopicId = topicId;
    }
  }
}