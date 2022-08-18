namespace Beon.Models.ViewModels {
  public class UserProfileLinkViewModel {
    public string UserName { get; set; }
    public string DisplayName { get; set; }

    public UserProfileLinkViewModel(string userName, string displayName) {
      UserName = userName;
      DisplayName = displayName;
    }
  }
}