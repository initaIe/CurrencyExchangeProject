namespace CurrencyExchange.Contracts.ExchangeRate.Requests;

public record UpdateExchangeRateRequest(Guid BaseCurrencyId, Guid TargetCurrencyId, decimal Rate);