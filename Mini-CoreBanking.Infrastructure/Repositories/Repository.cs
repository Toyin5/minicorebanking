using MiniCoreBanking.Application.Interfaces;
namespace MiniCoreBanking.Infrastructure.Repositories;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{

    protected readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public async Task<TEntity> GetAsync(Guid id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public IQueryable<TEntity> GetQuerable()
    {
        return _context.Set<TEntity>();
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }
}