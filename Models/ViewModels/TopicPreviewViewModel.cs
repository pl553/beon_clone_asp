namespace Beon.Models.ViewModels 
{
  public class TopicPreviewViewModel {
    public string Path { get; set; }
    public string Title { get; set; }
    public int PostCount { get; set; }
    public int OpId { get; set; }
    public TopicPreviewViewModel(string path, string title, int opId, int postCount) {
      Path = path;
      Title = title;
      OpId = opId;
      PostCount = postCount;
    }
  }
}