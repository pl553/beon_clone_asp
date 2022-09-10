namespace Beon.Models {
  public interface IRepository<TEntity> where TEntity : class {
    IQueryable<TEntity> Entities { get; }
    void Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
  }
}