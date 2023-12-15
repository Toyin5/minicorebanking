using MiniCoreBanking.Domain.Constants;

namespace MiniCoreBanking.Application.Dtos;

public record AccountDto(
    string Number,
    string Id,
    string CustomerID,
    decimal Balance,

    StatusTypes Status,


    AccountTypes Type,

    Currencies Currency,

    DateTime CreatedAt,
    DateTime UpdatedAt
);