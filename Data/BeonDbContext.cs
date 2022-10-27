using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Beon.Models {
  public class BeonDbContext : IdentityDbContext<BeonUser> {
    public IServiceProvider Provider { get; init; }

    public BeonDbContext(
      DbContextOptions<BeonDbContext> options,
      IServiceProvider provider)
    : base(options)
    {
      Provider = provider;
    }

    public T GetRequiredService<T>() where T : notnull
      => Provider.GetRequiredService<T>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.Entity<BeonUser>()
          .HasOne(u => u.Diary)
          .WithOne(d => d.Owner)
          .HasForeignKey<UserDiary>(d => d.OwnerId)
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
        
        builder.Entity<BeonUser>()
          .HasMany<TopicSubscription>()
          .WithOne(t => t.Subscriber)
          .IsRequired()
          .OnDelete(DeleteBehavior.Cascade);

    }
    public DbSet<Topic> Topics => Set<Topic>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Diary> Diaries => Set<Diary>();
    public DbSet<UserDiary> UserDiaries => Set<UserDiary>();
    public DbSet<UserDiaryEntry> UserDiaryEntries => Set<UserDiaryEntry>();
    public DbSet<DiaryEntryCategory> DiaryEntryCategories => Set<DiaryEntryCategory>();
    public DbSet<TopicSubscription> TopicSubscriptions => Set<TopicSubscription>();
    public DbSet<FriendLink> FriendLinks => Set<FriendLink>();
  }
}