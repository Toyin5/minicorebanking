using AutoMapper;
using MiniCoreBanking.Application.Dtos;
using MiniCoreBanking.Domain.Entities;
namespace MiniCoreBanking.Api;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Customer, CustomerDto>();
        CreateMap<Account, AccountDto>();
    }
}