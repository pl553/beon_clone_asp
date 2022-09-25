using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Beon.Models;

namespace Beon.Infrastructure
{
  public static class UserManagerExtensions {
    public static async Task<bool> IsAdminAsync(
      this UserManager<BeonUser> manager,
      ClaimsPrincipal? principal)
    {
      if (principal == null) return false;
      var user = await manager.GetUserAsync(principal);
      if (user == null) return false;
      return await manager.IsAdminAsync(user);
    }

    public static async Task<bool> IsAdminAsync(
      this UserManager<BeonUser> manager,
      BeonUser user)
    {
      var roles = await manager.GetRolesAsync(user);
      return roles.Contains("Admin");
    }

    public static async Task<BeonUser?> GetByUserNameAsync(
      this UserManager<BeonUser> manager,
      string userName)
    => await manager.Users.Where(u => u.UserName.Equals(userName)).FirstOrDefaultAsync();
  }
}