using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace Beon.Models
{
  public static class CreateAdmin
  {
    public static async void Create(IApplicationBuilder app)
    {
      var roles = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
      var users = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<BeonUser>>();
      bool x = await roles.RoleExistsAsync("Admin");
      if (!x) {
        var role = new IdentityRole();
        role.Name = "Admin";
        await roles.CreateAsync(role);

        var admin = new BeonUser();
        admin.UserName = "admin";
        await users.CreateAsync(admin, "123123");
        await users.AddToRoleAsync(admin, "Admin");
      }
    }
  }
}