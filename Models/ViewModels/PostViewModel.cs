namespace Beon.Models.ViewModels {
  public class PostViewModel {
    public int PostId { get; set; }
    public string BodyRawHtml { get; set; }
    public DateTime TimeStamp { get; set; }
    public PosterViewModel Poster { get; set; }
    public bool CanDelete { get; set; }
    public PostViewModel (
      int postId,
      string bodyRawHtml,
      DateTime timeStamp,
      PosterViewModel poster)
    {
      BodyRawHtml = bodyRawHtml;
      TimeStamp = timeStamp;
      Poster = poster;
      PostId = postId;
    }
  }
}