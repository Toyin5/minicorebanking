using MiniCoreBanking.Application.Models;
using MiniCoreBanking.Domain.Entities;

namespace MiniCoreBanking.Application;

public interface IJwtService
{
    public TokenResponse CreateToken(Customer user);


}