namespace MiniCoreBanking.Application.Models;

public class TransactionRequest
{
    public required string AccountNumber { get; set; }

    public decimal Amount { get; set; }

}