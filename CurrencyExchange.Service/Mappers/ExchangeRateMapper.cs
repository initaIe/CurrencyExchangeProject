using CurrencyExchange.Contracts.ExchangeRateContracts.DTOs;
using CurrencyExchange.Domain.Models;

namespace CurrencyExchange.Service.Mappers;

public static class ExchangeRateMapper
{
    public static ExchangeRateDTO ToExchangeRateDTO(ExchangeRate domainModel)
    {
        return new ExchangeRateDTO(
            domainModel.Id,
            CurrencyMapper.ToCurrencyDTO(domainModel.BaseCurrency),
            CurrencyMapper.ToCurrencyDTO(domainModel.TargetCurrency),
            domainModel.Rate);
    }
}