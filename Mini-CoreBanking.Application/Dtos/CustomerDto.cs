using MiniCoreBanking.Domain.Constants;

namespace MiniCoreBanking.Application.Dtos;
public record CustomerDto(
    string Email,
    string Address,
    string UserName,
    string FirstName,
    string LastName,
    long IdNumber,
    int IdType,
    string PhoneNumber,
    string Id,
    StatusTypes Status,
    DateTime LastLogin,
    DateTime CreatedAt,
    DateTime UpdatedAt
);