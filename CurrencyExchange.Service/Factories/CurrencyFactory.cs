using CurrencyExchange.Contracts.Currency.DTOs;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Result.Implementations;

namespace CurrencyExchange.Service.Factories;

public class CurrencyFactory
{
    public DomainModelCreationResult<Currency> Create(string code, string fullName, string sign)
    {
        return Currency.Create(
            Guid.NewGuid(),
            code,
            fullName,
            sign);
    }
}