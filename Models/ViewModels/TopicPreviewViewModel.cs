namespace Beon.Models.ViewModels 
{
  public class TopicPreviewViewModel {
    public string Path { get; set; }
    public string Title { get; set; }
    public int OpId { get; set; }
    public TopicPreviewViewModel(string path, string title, int opId) {
      Path = path;
      Title = title;
      OpId = opId;
    }
  }
}