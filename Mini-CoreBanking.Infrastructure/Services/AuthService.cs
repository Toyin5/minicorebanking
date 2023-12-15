using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiniCoreBanking.Application;
using MiniCoreBanking.Application.Interfaces;
using MiniCoreBanking.Application.Models;
using MiniCoreBanking.Application.Dtos;
using MiniCoreBanking.Application.Helpers;
using MiniCoreBanking.Domain.Constants;
using MiniCoreBanking.Domain.Entities;
using System.ComponentModel.DataAnnotations;
namespace MiniCoreBanking.Infrastructure;
public class AuthService : IAuthService
{
    private readonly IJwtService _jwtService;
    private readonly ILogger<AuthService> _logger;

    private readonly ICustomerRepository _repository;

    private readonly IMapper _mapper;

    public AuthService(IJwtService jwtService, ILogger<AuthService> logger,
       ICustomerRepository repository, IMapper mapper)
    {
        _jwtService = jwtService;
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CustomerDto> RegisterAsync(CustomerRequest request)
    {
        var CustomerExists = await _repository.GetQuerable().FirstOrDefaultAsync(customer => customer.Email == request.Email && customer.UserName == request.UserName);
        if (CustomerExists != null)
        {
            _logger.Log(LogLevel.Information, "duplicate credentials found");
            throw new Exception("Duplicate credentials found");
        }
        if (!new EmailAddressAttribute().IsValid(request.Email))
        {
            throw new Exception("Invalid email address");
        }
        var customer = new Customer()
        {
            Email = request.Email,
            Password = SecretHasher.Hash(request.Password),
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            IdNumber = request.IdNumber,
            IdType = (IDTypes)request.IdType,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            CreatedAt = DateTime.Now
        };
        await _repository.AddAsync(customer);
        await _repository.SaveChangesAsync();
        _logger.Log(LogLevel.Information, "A user is created");
        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<TokenResponse> LoginAsync(LoginRequest request)
    {
        var user = await _repository.GetQuerable().FirstOrDefaultAsync(customer => customer.Email == request.Email);

        if (!new EmailAddressAttribute().IsValid(request.Email))
        {
            throw new Exception("Invalid email address");
        }
        if (user == null)
        {
            _logger.Log(LogLevel.Warning, "Attempted login with invalid email address");
            throw new Exception("Invalid email/password");
        }
        user.LastLogin = DateTime.Now;

        var result = SecretHasher.Verify(request.Password, user.Password);
        if (result == false)
        {
            _logger.LogError($"There is an attempt to login user: {request.Email} with wrong password");
            throw new Exception("Wrong password!");
        }
        await _repository.SaveChangesAsync();
        var token = _jwtService.CreateToken(user);
        return token;
    }
}