using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiniCoreBanking.Domain.Entities;
using MiniCoreBanking.Domain.Constants;
using MiniCoreBanking.Application.Interfaces;
using MiniCoreBanking.Application.Dtos;
using MiniCoreBanking.Application.Models;
using MiniCoreBanking.Application.Helpers;
using MiniCoreBanking.Application.Extensions;

namespace MiniCoreBanking.Infrastructure;

public class AccountService : IAccountService
{

    private readonly IMapper _mapper;

    private readonly IAccountRepository _repository;

    private readonly ICustomerRepository _customerRepository;


    private readonly ILogger<AccountService> _logger;

    public AccountService(IMapper mapper, IAccountRepository repository, ILogger<AccountService> logger, ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
        _customerRepository = customerRepository;
    }
    public async Task<AccountDto> ActivateAccount(string accountNumber)
    {
        var account = await _repository.GetQuerable().SingleOrDefaultAsync(acc => acc.Number == accountNumber);
        if (account == null)
        {
            throw new Exception("Account not found");
        }
        account.Status = StatusTypes.ACTIVE;
        account.UpdatedAt = DateTime.Now;
        await _repository.SaveChangesAsync();
        _logger.Log(LogLevel.Information, $"Account: {accountNumber} has been activated ");
        return _mapper.Map<AccountDto>(account);
    }

    public async Task<AccountDto> CreateAccount(AccountRequest request)
    {
        var CustomerExists = await _customerRepository.GetAsync(request.CustomerID);
        if (CustomerExists == null)
        {
            throw new Exception("Customer not found");
        }
        var accountExists = await _repository.GetQuerable().SingleOrDefaultAsync(acc => acc.CustomerID == request.CustomerID && acc.Type == (AccountTypes)request.AccountType);
        if (accountExists != null)
        {
            throw new Exception("Duplicate Account Type");
        }
        var account = new Account()
        {
            CustomerID = request.CustomerID,
            Type = (AccountTypes)request.AccountType,
            Number = Generate.GenerateAccountNumber(),
            CreatedAt = DateTime.Now,
        };
        await _repository.AddAsync(account);
        await _repository.SaveChangesAsync();
        return _mapper.Map<AccountDto>(account);
    }

    public async Task<AccountDto> DeactivateAccount(string accountNumber)
    {
        var account = await _repository.GetQuerable().SingleOrDefaultAsync(acc => acc.Number == accountNumber);
        if (account == null)
        {
            throw new Exception("Account not found");
        }
        account.Status = StatusTypes.INACTIVE;
        account.UpdatedAt = DateTime.Now;
        await _repository.SaveChangesAsync();
        _logger.Log(LogLevel.Information, $"Account: {accountNumber} has been deactivated ");
        return _mapper.Map<AccountDto>(account);
    }

    public async Task<AccountDto> DeleteAccount(string accountNumber)
    {
        var account = await _repository.GetQuerable().SingleOrDefaultAsync(acc => acc.Number == accountNumber);
        if (account == null)
        {
            throw new Exception("Account not found");
        }
        _repository.Delete(account);
        await _repository.SaveChangesAsync();
        _logger.Log(LogLevel.Information, $"Account: {accountNumber} has been deleted ");
        return _mapper.Map<AccountDto>(account);
    }

    public async Task<AccountDto> Deposit(TransactionRequest request)
    {
        var account = await _repository.GetQuerable().SingleOrDefaultAsync(acc => acc.Number == request.AccountNumber);
        if (account == null)
        {
            throw new Exception("Account not found");
        }

        if (account.Status == StatusTypes.INACTIVE)
        {
            throw new Exception("Account is suspended!");
        }
        account.Deposit(request.Amount);
        account.UpdatedAt = DateTime.Now;
        await _repository.SaveChangesAsync();
        _logger.Log(LogLevel.Information, $"Account: {request.AccountNumber} has been credited ");
        return _mapper.Map<AccountDto>(account);
    }

    public async Task<AccountDto> GetAccount(string accountNumber)
    {
        var account = await _repository.GetQuerable().SingleOrDefaultAsync(acc => acc.Number == accountNumber);
        if (account == null)
        {
            throw new Exception("Account not found");
        }
        return _mapper.Map<AccountDto>(account);
    }

    public async Task<AccountDto> Withdraw(TransactionRequest request)
    {
        var account = await _repository.GetQuerable().SingleOrDefaultAsync(acc => acc.Number == request.AccountNumber);
        if (account == null)
        {
            throw new Exception("Account not found");
        }
        if (account.Status == StatusTypes.INACTIVE)
        {
            throw new Exception("Account is suspended!");
        }
        account.Withdraw(request.Amount);
        account.UpdatedAt = DateTime.Now;
        await _repository.SaveChangesAsync();
        _logger.Log(LogLevel.Information, $"Account: {request.AccountNumber} has been debited ");
        return _mapper.Map<AccountDto>(account);
    }
}