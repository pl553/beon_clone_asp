namespace Beon.Models.ViewModels {
  public class PosterViewModel {
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    //avatar image
    public PosterViewModel(string userName, string displayName) {
      UserName = userName;
      DisplayName = displayName;
    }
  }
}