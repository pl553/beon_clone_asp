using Microsoft.AspNetCore.Mvc;
using Beon.Models;
using Beon.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Beon.Components {
  public class DiaryUserInfoViewComponent : ViewComponent
  {
    private readonly UserManager<BeonUser> _userManager;

    public DiaryUserInfoViewComponent(UserManager<BeonUser> userManager) {
      _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync(string userName) {
      string? displayName = await _userManager.Users
        .Where(u => u.UserName.Equals(userName))
        .Select(u => u.DisplayName)
        .FirstOrDefaultAsync();

      if (displayName == null) {
        return View(new UserProfileLinkViewModel(userName, "error"));
      }

      return View(new UserProfileLinkViewModel(userName, displayName));
    }
  }
}