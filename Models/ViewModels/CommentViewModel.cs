namespace Beon.Models.ViewModels {
  public class CommentViewModel {
    public PostViewModel Post { get; set; }
    public bool CanDelete { get; set; }
    public CommentViewModel (PostViewModel post, bool canDelete)
    {
      Post = post;
      CanDelete = canDelete;
    }
  }
}