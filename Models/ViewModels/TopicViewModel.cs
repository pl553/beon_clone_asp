namespace Beon.Models.ViewModels 
{
  public class TopicViewModel {
    public string PostCreatePath { get; set; }
    public string Title { get; set; }
    public PostViewModel Op { get; set; }
    public ICollection<CommentViewModel> Comments { get; set; }
    public int TopicId { get; set; }
    public bool CanEdit { get; set; }
    public TopicViewModel(string postCreatePath, int topicId, string title, PostViewModel op, ICollection<CommentViewModel> comments, bool canEdit = false) {
      Title = title;
      PostCreatePath = postCreatePath;
      Op = op;
      Comments = comments;
      TopicId = topicId;
      CanEdit = canEdit;
    }
  }
}