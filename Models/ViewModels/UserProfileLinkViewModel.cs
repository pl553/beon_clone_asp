namespace Beon.Models.ViewModels {
  public class UserProfileLinkViewModel {
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public bool Valid { get; set; }

    public UserProfileLinkViewModel(string userName, string displayName) {
      UserName = userName;
      DisplayName = displayName;
      Valid = true;
    }

    public UserProfileLinkViewModel() {
      UserName = "";
      DisplayName = "";
      Valid = false;
    }
  }
}