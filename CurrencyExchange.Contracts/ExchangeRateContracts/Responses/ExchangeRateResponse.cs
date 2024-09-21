using CurrencyExchange.Contracts.CurrencyContracts.Responses;

namespace CurrencyExchange.Contracts.ExchangeRateContracts.Responses;

public record ExchangeRateResponse(
    Guid Id,
    CurrencyResponse BaseCurrency,
    CurrencyResponse TargetCurrency,
    decimal Rate);