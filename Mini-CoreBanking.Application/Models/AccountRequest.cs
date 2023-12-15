namespace MiniCoreBanking.Application.Models;

public class AccountRequest
{
    public Guid CustomerID { get; set; }
    public int AccountType { get; set; }

    public int AccountCurrency { get; set; }
}