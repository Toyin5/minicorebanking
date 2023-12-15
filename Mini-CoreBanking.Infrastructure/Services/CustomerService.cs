using MiniCoreBanking.Domain;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MiniCoreBanking.Application.Interfaces;
using MiniCoreBanking.Application.Models;
using MiniCoreBanking.Application.Dtos;
using MiniCoreBanking.Domain.Constants;
using MiniCoreBanking.Application.Extensions;

namespace MiniCoreBanking.Application;

public class CustomerService : ICustomerService
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _repository;
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(IMapper mapper, ILogger<CustomerService> logger, ICustomerRepository repository)
    {
        _mapper = mapper;
        _logger = logger;
        _repository = repository;
    }
    public async Task<CustomerDto> UpdateCustomer(UpdateCustomerRequest request, Guid id)
    {
        var customer = await _repository.GetAsync(id);
        if (customer == null)
        {
            throw new Exception("Customer not found");
        }
        customer.Update(request);
        customer.UpdatedAt = DateTime.Now;
        await _repository.SaveChangesAsync();
        return _mapper.Map<CustomerDto>(customer);
    }
    public async Task<CustomerDto> DeleteCustomer(Guid id)
    {
        var customer = await _repository.GetAsync(id);
        if (customer == null)
        {
            throw new Exception("Customer not found");
        }
        _repository.Delete(customer);
        await _repository.SaveChangesAsync();
        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<CustomerDto> ActivateCustomer(Guid id)
    {
        var customer = await _repository.GetAsync(id); ;
        if (customer == null)
        {
            throw new Exception("Customer not found");
        }
        customer.Status = StatusTypes.ACTIVE;
        customer.UpdatedAt = DateTime.Now;
        await _repository.SaveChangesAsync();
        return _mapper.Map<CustomerDto>(customer);
    }
    public async Task<CustomerDto> DeactivateCustomer(Guid id)
    {
        var customer = await _repository.GetAsync(id);
        if (customer == null)
        {
            throw new Exception("Customer not found");
        }
        customer.Status = StatusTypes.INACTIVE;
        customer.UpdatedAt = DateTime.Now;
        await _repository.SaveChangesAsync();
        return _mapper.Map<CustomerDto>(customer);

    }
}