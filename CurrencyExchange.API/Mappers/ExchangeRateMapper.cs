using CurrencyExchange.Contracts.ExchangeRateContracts.DTOs;
using CurrencyExchange.Contracts.ExchangeRateContracts.Requests;
using CurrencyExchange.Contracts.ExchangeRateContracts.Responses;

namespace CurrencyExchange.API.Mappers;

public static class ExchangeRateMapper
{
    public static ExchangeRateResponse ToExchangeRateResponse(this ExchangeRateDTO dto)
    {
        return new ExchangeRateResponse(
            dto.Id,
            dto.BaseCurrency.ToCurrencyResponse(),
            dto.TargetCurrency.ToCurrencyResponse(),
            dto.Rate);
    }

    public static CreateExchangeRateDTO ToCreateExchangeRateDTO(this CreateExchangeRateRequest request)
    {
        return new CreateExchangeRateDTO(
            request.BaseCurrencyId,
            request.TargetCurrencyId,
            request.Rate);
    }

    public static UpdateExchangeRateDTO ToUpdateExchangeRateDTO(this UpdateExchangeRateRequest request)
    {
        return new UpdateExchangeRateDTO(
            request.BaseCurrencyId,
            request.TargetCurrencyId,
            request.Rate);
    }
}