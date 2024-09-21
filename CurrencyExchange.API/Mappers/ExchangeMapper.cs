using CurrencyExchange.Contracts.ExchangeContracts.DTOs;
using CurrencyExchange.Contracts.ExchangeContracts.Requests;
using CurrencyExchange.Contracts.ExchangeContracts.Responses;

namespace CurrencyExchange.API.Mappers;

public static class ExchangeMapper
{
    public static GetExchangeDTO ToGetExchangeDTO(this GetExchangeRequest request)
    {
        return new GetExchangeDTO(
            request.From,
            request.To,
            request.Amount);
    }

    public static ExchangeResponse ToExchangeResponse(this ExchangeDTO dto)
    {
        return new ExchangeResponse(
            dto.BaseCurrency.ToCurrencyResponse(),
            dto.TargetCurrency.ToCurrencyResponse(),
            dto.Rate,
            dto.Amount,
            dto.ConvertedAmount);
    }
}