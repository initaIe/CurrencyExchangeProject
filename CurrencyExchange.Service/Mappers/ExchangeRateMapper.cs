using CurrencyExchange.Contracts.ExchangeRate.DTOs;
using CurrencyExchange.Contracts.ExchangeRate.Requests;
using CurrencyExchange.DAL.Entities;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Result.Implementations;
using CurrencyExchange.Service.Factories;

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
    
    public static ExchangeRateDTO ToExchangeRateDTO(ExchangeRateEntity entity)
    {
        return new ExchangeRateDTO(
            Guid.Parse(entity.Id),
            CurrencyMapper.ToCurrencyDTO(entity.BaseCurrency),
            CurrencyMapper.ToCurrencyDTO(entity.TargetCurrency),
            entity.Rate);
    }
}