namespace Beon.Models.ViewModels {
  public class UserProfileLinkViewModel {
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public bool Anonymous { get; set; }

    public UserProfileLinkViewModel(string userName, string displayName) {
      UserName = userName;
      DisplayName = displayName;
      Anonymous = false;
    }

    public UserProfileLinkViewModel(PosterViewModel? poster)
    {
      if (poster == null)
      {
        Anonymous = true;
        UserName = DisplayName = "";
      }
      else
      {
        UserName = poster.UserName;
        DisplayName = poster.DisplayName;
        Anonymous = false;
      }
    }
  
  }
}