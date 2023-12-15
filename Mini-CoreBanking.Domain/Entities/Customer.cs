using MiniCoreBanking.Domain.Common;
using MiniCoreBanking.Domain.Constants;

namespace MiniCoreBanking.Domain.Entities;
public class Customer : BaseEntity
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string UserName { get; set; }
    public required string Address { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public long IdNumber { get; set; }
    public IDTypes IdType { get; set; }
    public required string PhoneNumber { get; set; }

    public DateTime LastLogin { get; set; }

    public ICollection<Account>? Accounts;
}
