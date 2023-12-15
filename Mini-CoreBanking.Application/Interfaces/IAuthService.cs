using MiniCoreBanking.Application.Dtos;
using MiniCoreBanking.Application.Models;

namespace MiniCoreBanking.Application.Interfaces;
public interface IAuthService
{
    public Task<CustomerDto> RegisterAsync(CustomerRequest request);
    public Task<TokenResponse> LoginAsync(LoginRequest request);
}