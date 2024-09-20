using CurrencyExchange.Contracts.Currency.DTOs;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Result.Implementations;

namespace CurrencyExchange.Service.Mappers;

public static class CurrencyMapper
{
    public static CurrencyDTO ToCurrencyDTO(Currency domainModel)
    {
        return new CurrencyDTO(
            domainModel.Id,
            domainModel.Code,
            domainModel.FullName,
            domainModel.Sign);
    }

    public static Result<Currency> ToCurrency(Guid id, UpdateCurrencyDTO dto)
    {
        return Currency.Create(id, dto.Code, dto.FullName, dto.Sign);
    }
}