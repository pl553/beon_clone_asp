using Microsoft.EntityFrameworkCore;
  
namespace Beon.Models {
  public class BeonDbContext : DbContext {
    public BeonDbContext(DbContextOptions<BeonDbContext> options)
    : base(options) { }

    public DbSet<Board> Boards => Set<Board>();
  }
}