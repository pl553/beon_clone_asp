namespace Beon.Models.ViewModels {
  public record UserProfileViewModel(
    string UserName,
    string DisplayName,
    IEnumerable<UserProfileLinkViewModel> Friends,
    IEnumerable<UserProfileLinkViewModel> FriendOf,
    IEnumerable<UserProfileLinkViewModel> Mutuals,
    bool ShowFriendAddingUI,
    bool ProfileUserIsFriend
  );
}