using CurrencyExchange.Contracts.Currency.Responses;

namespace CurrencyExchange.Contracts.Exchange.Responses;

public record ExchangeResponse(
    CurrencyResponse BaseCurrency,
    CurrencyResponse TargetCurrency,
    decimal Rate,
    decimal Amount,
    decimal ConvertedAmount);