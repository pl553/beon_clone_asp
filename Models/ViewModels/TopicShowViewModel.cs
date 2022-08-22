namespace Beon.Models.ViewModels 
{
  public class TopicShowViewModel {
    public string PostCreatePath { get; set; }
    public string Title { get; set; }
    public ICollection<int> PostIds { get; set; }
    public int TopicId { get; set; }
    
    public TopicShowViewModel(string postCreatePath, int topicId, string title, ICollection<int> postIds) {
      Title = title;
      PostCreatePath = postCreatePath;
      PostIds = postIds;
      TopicId = topicId;
    }
  }
}