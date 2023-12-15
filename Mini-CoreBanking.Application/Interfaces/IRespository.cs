namespace MiniCoreBanking.Application.Interfaces;
public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetAsync(Guid id);
    Task AddAsync(TEntity entity);

    Task<int> SaveChangesAsync();

    IQueryable<TEntity> GetQuerable();

    void Delete(TEntity entity);
}