using CurrencyExchange.Contracts.Currency.DTOs;
using CurrencyExchange.DAL.Entities;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Result.Implementations;
using CurrencyExchange.Service.Factories;

namespace CurrencyExchange.Service.Mappers;

public static class CurrencyMapper
{
    public static DomainModelCreationResult<Currency> ToCurrencyDomainModel(CurrencyEntity entity)
    {
        return Currency.Create(Guid.Parse(entity.Id), entity.Code, entity.FullName, entity.Sign);
    }

    public static DomainModelCreationResult<Currency> ToCurrencyDomainModel(Guid id, UpdateCurrencyDTO dto)
    {
        return Currency.Create(
            id,
            dto.Code,
            dto.FullName,
            dto.Sign);
    }
    
    public static CurrencyDTO ToCurrencyDTO(CurrencyEntity entity)
    {
        return new CurrencyDTO(
            Guid.Parse(entity.Id),
            entity.Code,
            entity.FullName,
            entity.Sign);
    }
    
    public static CurrencyDTO ToCurrencyDTO(Currency domainModel)
    {
        return new CurrencyDTO(
            domainModel.Id,
            domainModel.Code,
            domainModel.FullName,
            domainModel.Sign);
    }
}