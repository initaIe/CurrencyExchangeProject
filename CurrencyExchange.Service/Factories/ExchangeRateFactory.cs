using System.Security.Cryptography.X509Certificates;
using CurrencyExchange.DAL.Entities;
using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Result.Implementations;
using CurrencyExchange.Service.Interfaces;
using CurrencyExchange.Service.Mappers;

namespace CurrencyExchange.Service.Factories;

public class ExchangeRateFactory(IRepository<Currency, CurrencyEntity> currencyRepository)
{
    public async Task<DomainModelCreationResult<ExchangeRate>> Create(
        Guid? id,
        Guid baseCurrencyId,
        Guid targetCurrencyId,
        decimal rate)
    {
        var baseCurrencyDbResult = await currencyRepository.GetByIdAsync(baseCurrencyId);
        var targetCurrencyDbResult = await currencyRepository.GetByIdAsync(targetCurrencyId);

        List<string> errors = [];

        if (!baseCurrencyDbResult.IsSuccess || baseCurrencyDbResult.Data == null)
            errors.Add("Base currency not found");

        if (!targetCurrencyDbResult.IsSuccess || targetCurrencyDbResult.Data == null)
            errors.Add("Target currency not found");

        if (errors.Count > 0)
            return DomainModelCreationResult<ExchangeRate>.Failure(errors);

        // Нет валидации т.к. это полученные из бд, при получении из бд нет проверки
        var baseCurrency = CurrencyMapper.ToCurrencyDomainModel(
            baseCurrencyDbResult.Data!).Data;
        var targetCurrency = CurrencyMapper.ToCurrencyDomainModel(
            targetCurrencyDbResult.Data!).Data;
        
        var guid = id ?? Guid.NewGuid();
        
        return ExchangeRate.Create(guid, baseCurrency!, targetCurrency!, rate);
    }
}