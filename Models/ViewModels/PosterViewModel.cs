namespace Beon.Models.ViewModels {
  public class PosterViewModel {
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public string? AvatarFilePath { get; set; }
    public PosterViewModel(string userName, string displayName, string? avatarFilePath) {
      UserName = userName;
      DisplayName = displayName;
      AvatarFilePath = avatarFilePath;
    }
  }
}