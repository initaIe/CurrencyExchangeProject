using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Result.Implementations;

namespace CurrencyExchange.Service.Factories;

public class ExchangeRateFactory(ICurrencyRepository currencyRepository)
{
    public async Task<Result<ExchangeRate>> Create(
        Guid? id,
        Guid baseCurrencyId,
        Guid targetCurrencyId,
        decimal rate)
    {
        List<string> errors = [];

        var baseCurrencyDbResult = await currencyRepository.GetByIdAsync(baseCurrencyId);

        if (!baseCurrencyDbResult.IsSuccess || baseCurrencyDbResult.Data == null)
            errors.Add("Base currency not found");

        var targetCurrencyDbResult = await currencyRepository.GetByIdAsync(targetCurrencyId);

        if (!targetCurrencyDbResult.IsSuccess || targetCurrencyDbResult.Data == null)
            errors.Add("Target currency not found");

        if (errors.Count > 0)
            return Result<ExchangeRate>.Failure(errors);

        var guid = id ?? Guid.NewGuid();

        return ExchangeRate.Create(guid, baseCurrencyDbResult.Data!, targetCurrencyDbResult.Data!, rate);
    }
}