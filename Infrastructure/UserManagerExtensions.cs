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
      var roles = await manager.GetRolesAsync(await manager.GetUserAsync(principal));
      return roles.Contains("Admin");
    }
  }
}