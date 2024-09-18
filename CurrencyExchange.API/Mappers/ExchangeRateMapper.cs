using CurrencyExchange.Contracts.Currency.Responses;
using CurrencyExchange.Contracts.ExchangeRate.DTOs;
using CurrencyExchange.Contracts.ExchangeRate.Requests;
using CurrencyExchange.Contracts.ExchangeRate.Responses;

namespace CurrencyExchange.API.Mappers;

public class ExchangeRateMapper
{
    public static ExchangeRateResponse ToExchangeRateResponse(ExchangeRateDTO dto)
    {
        return new ExchangeRateResponse(
            dto.Id,
            new CurrencyResponse(
                dto.BaseCurrency.Id,
                dto.BaseCurrency.Code,
                dto.BaseCurrency.FullName,
                dto.BaseCurrency.Sign),
            new CurrencyResponse(
                dto.TargetCurrency.Id,
                dto.TargetCurrency.Code,
                dto.TargetCurrency.FullName,
                dto.TargetCurrency.Sign),
            dto.Rate);
    }

    public static CreateExchangeRateDTO ToCreateExchangeRateDTO(CreateExchangeRateRequest request)
    {
        return new CreateExchangeRateDTO(
            request.BaseCurrencyId,
            request.TargetCurrencyId,
            request.Rate);
    }

    public static UpdateExchangeRateDTO ToUpdateExchangeRateDTO(UpdateExchangeRateRequest request)
    {
        return new UpdateExchangeRateDTO(
            request.BaseCurrencyId,
            request.TargetCurrencyId,
            request.Rate);
    }
}