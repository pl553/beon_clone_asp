namespace Beon.Models.ViewModels {
  public class PostViewModel {
    public bool ShowDate { get; set; }
    public string BodyRawHtml { get; set; }
    public DateTime TimeStamp { get; set; }
    public PosterViewModel Poster { get; set; }
    public bool CanDelete { get; set; }
    public PostViewModel (
      string bodyRawHtml,
      DateTime timeStamp,
      PosterViewModel poster,
      bool showDate = false,
      bool canDelete = false)
    {
      BodyRawHtml = bodyRawHtml;
      TimeStamp = timeStamp;
      Poster = poster;
      ShowDate = showDate;
      CanDelete = canDelete;
    }
  }
}