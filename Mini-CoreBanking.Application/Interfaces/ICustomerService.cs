using MiniCoreBanking.Application.Dtos;
using MiniCoreBanking.Application.Models;

namespace MiniCoreBanking.Application.Interfaces;
public interface ICustomerService
{
    public Task<CustomerDto> UpdateCustomer(UpdateCustomerRequest request, Guid id);
    public Task<CustomerDto> ActivateCustomer(Guid id);
    public Task<CustomerDto> DeactivateCustomer(Guid id);
    // public Task<CustomerDto>[] GetCustomers();
    public Task<CustomerDto> DeleteCustomer(Guid id);

}