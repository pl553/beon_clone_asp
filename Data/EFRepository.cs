using Microsoft.EntityFrameworkCore;

namespace Beon.Models {
  public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class {
    private BeonDbContext context;
    public EFRepository(BeonDbContext ctx) {
      context = ctx;
    }

    public void Create(TEntity entity) {
      context.Set<TEntity>().Add(entity);
      context.SaveChanges();
    }
    public void Update(TEntity entity) {
      context.Entry(entity).State = EntityState.Modified;
      context.SaveChanges();
    }
    public void Delete(TEntity entity) {
      context.Entry(entity).State = EntityState.Deleted;
      context.SaveChanges();
    }
    public async Task CreateAsync(TEntity entity) {
      context.Set<TEntity>().Add(entity);
      await context.SaveChangesAsync();
    }
    public async Task UpdateAsync(TEntity entity) {
      context.Entry(entity).State = EntityState.Modified;
      await context.SaveChangesAsync();
    }
    public async Task DeleteAsync(TEntity entity) {
      context.Entry(entity).State = EntityState.Deleted;
      await context.SaveChangesAsync();
    }
    public IQueryable<TEntity> Entities => context.Set<TEntity>();
  }
}