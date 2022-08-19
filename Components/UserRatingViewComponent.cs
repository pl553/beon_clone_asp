using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Beon.Components {
  public class UserRatingViewComponent : ViewComponent
  {
    private readonly UserManager<BeonUser> _userManager;

    public UserRatingViewComponent(UserManager<BeonUser> userManager) {
      _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync() {
      ICollection<UserProfileLinkViewModel> users = new List<UserProfileLinkViewModel>();
        var dt = await _userManager.Users
          .Select(u => new { u.UserName, u.DisplayName })
          .ToListAsync();

        foreach (var u in dt)
        {
          users.Add(new UserProfileLinkViewModel(u.UserName, u.DisplayName));
        }

        return View(users);
    }
  }
}