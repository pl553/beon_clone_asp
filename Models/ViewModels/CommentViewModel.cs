namespace Beon.Models.ViewModels {
  public class CommentViewModel {
    public PostViewModel Post { get; set; }
    public string TopicLink { get; set; }
    public bool CanDelete { get; set; }
    public CommentViewModel (PostViewModel post, string topicLink, bool canDelete)
    {
      Post = post;
      TopicLink = topicLink;
      CanDelete = canDelete;
    }
  }
}