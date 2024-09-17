using CurrencyExchange.Contracts.Currency.DTOs;
using CurrencyExchange.Contracts.Currency.Requests;
using CurrencyExchange.Contracts.Currency.Responses;
using CurrencyExchange.Domain.Models;

namespace CurrencyExchange.API.Mappers;

public static class CurrencyMapper
{
    public static CurrencyResponse ToCurrencyResponse(CurrencyDTO dto)
    {
        return new CurrencyResponse(
            dto.Id,
            dto.Code,
            dto.FullName,
            dto.Sign);
    }
    
    public static CreateCurrencyDTO ToCreateCurrencyDTO(CreateCurrencyRequest request)
    {
        return new CreateCurrencyDTO(
            request.Code,
            request.FullName,
            request.Sign);
    }
    
    public static UpdateCurrencyDTO ToUpdateCurrencyDTO(UpdateCurrencyRequest request)
    {
        return new UpdateCurrencyDTO(
            request.Code,
            request.FullName,
            request.Sign);
    }
}