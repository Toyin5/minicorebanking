using MiniCoreBanking.Domain.Common;
using MiniCoreBanking.Domain.Constants;

namespace MiniCoreBanking.Domain.Entities;
public class Account : BaseEntity
{
    public required string Number { get; set; }
    public Guid CustomerID { get; set; }
    public decimal Balance { get; set; }


    public AccountTypes Type { get; set; }

    public Currencies Currency { get; set; }
}