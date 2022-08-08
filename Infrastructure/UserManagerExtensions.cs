using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Beon.Infrastructure
{
  public static class UserManagerExtensions {
    public static async Task<bool> IsAdmin(this UserManager<IdentityUser> manager, ClaimsPrincipal principal) {
       var roles = await manager.GetRolesAsync(await manager.GetUserAsync(principal));
       return roles.Contains("Admin");
    }
  }
}