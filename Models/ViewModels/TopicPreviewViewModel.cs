namespace Beon.Models.ViewModels 
{
  public class TopicPreviewViewModel {
    public int TopicId { get; set; }
    public string Path { get; set; }
    public string Title { get; set; }
    public int CommentCount { get; set; }
    public PostViewModel Op { get; set; }
    public bool CanEdit { get; set; }
    public TopicPreviewViewModel(
      int topicId,
      string path,
      string title,
      PostViewModel op,
      int commentCount,
      bool canEdit = false)
    {
      Path = path;
      Title = title;
      Op = op;
      CommentCount = commentCount;
      CanEdit = canEdit;
      TopicId = topicId;
    }
  }
}