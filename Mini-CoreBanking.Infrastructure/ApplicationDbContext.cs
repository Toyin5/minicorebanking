using Microsoft.EntityFrameworkCore;
using MiniCoreBanking.Domain.Entities;

namespace MiniCoreBanking.Infrastructure;
public class ApplicationDbContext : DbContext
{


    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("mini-banking");
        // optionsBuilder.UseSqlite(@"Data Source=./minicorebanking.db;");
    }
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Account> Accounts => Set<Account>();



}
