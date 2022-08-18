namespace Beon.Models.ViewModels {
  public class PostShowViewModel {
    public bool ShowDate { get; set; }
    public string Body { get; set; }
    public DateTime TimeStamp { get; set; }
    public PosterViewModel Poster { get; set; }
    public PostShowViewModel (string body, DateTime timeStamp, PosterViewModel poster, bool showDate = false) {
      Body = body;
      TimeStamp = timeStamp;
      Poster = poster;
      ShowDate = showDate;
    }
  }
}