namespace Beon.Models.ViewModels {
  public class PostShowViewModel {
    public string Body { get; set; }
    public DateTime TimeStamp { get; set; }
    public PosterViewModel Poster { get; set; }
    public PostShowViewModel (string body, DateTime timeStamp, PosterViewModel poster) {
      Body = body;
      TimeStamp = timeStamp;
      Poster = poster;
    }
  }
}