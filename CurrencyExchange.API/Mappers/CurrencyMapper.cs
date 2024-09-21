using CurrencyExchange.Contracts.CurrencyContracts.DTOs;
using CurrencyExchange.Contracts.CurrencyContracts.Requests;
using CurrencyExchange.Contracts.CurrencyContracts.Responses;

namespace CurrencyExchange.API.Mappers;

public static class CurrencyMapper
{
    public static CurrencyResponse ToCurrencyResponse(this CurrencyDTO dto)
    {
        return new CurrencyResponse(
            dto.Id,
            dto.Code,
            dto.FullName,
            dto.Sign);
    }

    public static CreateCurrencyDTO ToCreateCurrencyDTO(this CreateCurrencyRequest request)
    {
        return new CreateCurrencyDTO(
            request.Code,
            request.FullName,
            request.Sign);
    }

    public static UpdateCurrencyDTO ToUpdateCurrencyDTO(this UpdateCurrencyRequest request)
    {
        return new UpdateCurrencyDTO(
            request.Code,
            request.FullName,
            request.Sign);
    }
}