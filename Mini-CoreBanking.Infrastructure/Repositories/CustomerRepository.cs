using MiniCoreBanking.Application.Interfaces;
using MiniCoreBanking.Domain.Entities;
namespace MiniCoreBanking.Infrastructure.Repositories;
public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext _context) : base(_context)
    {
    }
}