namespace Beon.Models.ViewModels {
  public class HomeViewModel {
    public ICollection<UserProfileLinkViewModel> Users { get; set; }
    public HomeViewModel(ICollection<UserProfileLinkViewModel> users) {
      Users = users;
    }
  }
}