using MiniCoreBanking.Application.Interfaces;
using MiniCoreBanking.Domain.Entities;
namespace MiniCoreBanking.Infrastructure.Repositories;
public class AccountRepository : Repository<Account>, IAccountRepository
{
    public AccountRepository(ApplicationDbContext _context) : base(_context)
    {
    }
}