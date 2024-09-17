namespace CurrencyExchange.Contracts.ExchangeRate.Requests;

public record CreateExchangeRateRequest(Guid BaseCurrencyId, Guid TargetCurrencyId, decimal Rate);