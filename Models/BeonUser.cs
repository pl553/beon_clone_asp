using Microsoft.AspNetCore.Identity;

namespace Beon.Models {
  public class BeonUser : IdentityUser {
      //public ICollection<BeonUser> Friends { get; set; } = new List<BeonUser>();
      public string DisplayName { get; set; } = "default";
      public Diary? Diary { get; set; }
  }
}