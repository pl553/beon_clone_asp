namespace Beon.Models.ViewModels 
{
  public class TopicPreviewViewModel {
    public BoardType BoardType { get; set; }
    public string BoardOwnerName { get; set; }
    public int TopicOrd { get; set; }
    public string Title { get; set; }
    public PostShowViewModel Op { get; set; }
    public TopicPreviewViewModel(BoardType boardType, string boardOwnerName, int topicOrd, string title, PostShowViewModel op) {
      BoardType = boardType;
      BoardOwnerName = boardOwnerName;
      Title = title;
      TopicOrd = topicOrd;
      Op = op;
    }
  }
}