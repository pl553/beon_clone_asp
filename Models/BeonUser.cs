using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Beon.Models {
  public class BeonUser : IdentityUser {
      public string DisplayName { get; set; } = "default";
      public string? AvatarFileName { get; set; } = null;
      public Diary? Diary { get; set; }
  }
}