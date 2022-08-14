using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Beon.Models {
  public class BeonDbContext : IdentityDbContext<BeonUser> {
    public BeonDbContext(DbContextOptions<BeonDbContext> options)
    : base(options) { }

    public DbSet<Board> Boards => Set<Board>();
    public DbSet<Topic> Topics => Set<Topic>();
    public DbSet<Post> Posts => Set<Post>();
  }
}