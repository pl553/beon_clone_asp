namespace Beon.Models.ViewModels 
{
  public class BoardShowViewModel {
    public ICollection<TopicPreviewViewModel> Topics { get; set; }
    public BoardType BoardType;
    public string BoardOwnerName;

    public BoardShowViewModel(BoardType boardType, string boardOwnerName, ICollection<TopicPreviewViewModel> topics) {
      Topics = topics;
      BoardType = boardType;
      BoardOwnerName = boardOwnerName;
    }
  }
}