using Microsoft.Extensions.DependencyInjection;
using MiniCoreBanking.Application;
using MiniCoreBanking.Application.Interfaces;
using MiniCoreBanking.Infrastructure.Repositories;

namespace MiniCoreBanking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        return services;
    }

}