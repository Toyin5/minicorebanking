using MiniCoreBanking.Application.Models;
using MiniCoreBanking.Domain.Entities;

namespace MiniCoreBanking.Application.Extensions;
public static class CustomerExtensions
{
    public static void Update(this Customer customer, UpdateCustomerRequest request)
    {
        customer.PhoneNumber = request.PhoneNumber;
        customer.FirstName = request.FirstName;
        customer.LastName = request.LastName;
        customer.Address = request.Address;
        customer.UserName = request.UserName;
    }


}