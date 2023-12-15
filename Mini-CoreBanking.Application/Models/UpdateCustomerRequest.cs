namespace MiniCoreBanking.Application.Models;
public class UpdateCustomerRequest
{

    public required string UserName { get; set; }
    public required string Address { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
}