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
          .HasForeignKey<Diary>(d => d.OwnerId)
          .IsRequired();

        builder.Entity<Board>()
          .HasOne<PublicForum>()
          .WithOne(f => f.Board)
          .IsRequired();

        builder.Entity<Topic>()
          .HasMany<TopicSubscription>()
          .WithOne(t => t.Topic)
          .IsRequired()
          .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Topic>()
          .HasMany<Comment>(t => t.Comments)
          .WithOne(c => c.Topic)
          .IsRequired()
          .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Topic>()
          .HasOne<OriginalPost>(t => t.OriginalPost)
          .WithOne(op => op.Topic)
          .HasForeignKey<OriginalPost>(op => op.TopicId)
          .IsRequired()
          .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<BeonUser>()
          .HasMany<TopicSubscription>()
          .WithOne(t => t.Subscriber)
          .IsRequired()
          .OnDelete(DeleteBehavior.Cascade);

    }

    public DbSet<Board> Boards => Set<Board>();
    public DbSet<Topic> Topics => Set<Topic>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<OriginalPost> OriginalPosts => Set<OriginalPost>();
    public DbSet<Diary> Diaries => Set<Diary>();
    public DbSet<TopicSubscription> TopicSubscriptions => Set<TopicSubscription>();
  }
}