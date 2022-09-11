namespace Beon.Models.ViewModels 
{
  public class TopicViewModel {
    public string PostCreatePath { get; set; }
    public string Title { get; set; }
    public ICollection<PostViewModel> Posts { get; set; }
    public int TopicId { get; set; }
    public bool CanEdit { get; set; }
    public TopicViewModel(string postCreatePath, int topicId, string title, ICollection<PostViewModel> posts, bool canEdit = false) {
      Title = title;
      PostCreatePath = postCreatePath;
      Posts = posts;
      TopicId = topicId;
      CanEdit = canEdit;
    }
  }
}