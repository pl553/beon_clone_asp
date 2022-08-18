namespace Beon.Models.ViewModels 
{
  public class TopicShowViewModel {
    public BoardType BoardType { get; set; }
    public string BoardOwnerName { get; set; }
    public int TopicOrd { get; set; }
    public string Title { get; set; }
    public ICollection<PostShowViewModel> Posts { get; set; }
    
    public TopicShowViewModel(BoardType boardType, string boardOwnerName, int topicOrd, string title, ICollection<PostShowViewModel> posts) {
      BoardType = boardType;
      BoardOwnerName = boardOwnerName;
      TopicOrd = topicOrd;
      Title = title;
      Posts = posts;
    }
  }
}