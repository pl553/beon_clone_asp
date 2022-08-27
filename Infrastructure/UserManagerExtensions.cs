using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Beon.Models;

namespace Beon.Infrastructure
{
  public static class UserManagerExtensions {
    public static async Task<bool> IsAdmin(this UserManager<BeonUser> manager, ClaimsPrincipal? principal) {
      if (principal == null) return false;
      var user = await manager.GetUserAsync(principal);
      if (user == null) return false;
      return await manager.IsAdmin(user);
    }

    public static async Task<bool> IsAdmin(this UserManager<BeonUser> manager, BeonUser user) {
      var roles = await manager.GetRolesAsync(user);
      return roles.Contains("Admin");
    }

    public static BeonUser? GetByUserName(this UserManager<BeonUser> manager, string userName) {
       return manager.Users.Where(u => u.UserName.Equals(userName)).FirstOrDefault();
    }
  }
}