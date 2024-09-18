using CurrencyExchange.Contracts.Currency.Responses;

namespace CurrencyExchange.Contracts.ExchangeRate.Responses;

public record ExchangeRateResponse(
    Guid Id,
    CurrencyResponse BaseCurrency,
    CurrencyResponse TargetCurrency,
    decimal Rate);