using CurrencyExchange.Contracts.CurrencyContracts.Responses;

namespace CurrencyExchange.Contracts.ExchangeContracts.Responses;

public record ExchangeResponse(
    CurrencyResponse BaseCurrency,
    CurrencyResponse TargetCurrency,
    decimal Rate,
    decimal Amount,
    decimal ConvertedAmount);