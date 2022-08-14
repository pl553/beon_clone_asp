using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Beon.Models {
  public class BeonDbContext : IdentityDbContext<BeonUser> {
    public BeonDbContext(DbContextOptions<BeonDbContext> options)
    : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.Entity<BeonUser>()
          .HasOne(u => u.Diary)
          .WithOne(d => d.Owner)
          .HasForeignKey<Diary>(d => d.OwnerId);
    }

    public DbSet<Board> Boards => Set<Board>();
    public DbSet<Topic> Topics => Set<Topic>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Diary> Diaries => Set<Diary>();
  }
}