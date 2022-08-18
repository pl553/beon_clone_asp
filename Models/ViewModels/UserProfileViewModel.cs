namespace Beon.Models.ViewModels {
  public class UserProfileViewModel {
    public string UserName { get; set; }
    public string DisplayName { get; set; }

    public UserProfileViewModel(string userName, string displayName) {
      UserName = userName;
      DisplayName = displayName;
    }
  }
}