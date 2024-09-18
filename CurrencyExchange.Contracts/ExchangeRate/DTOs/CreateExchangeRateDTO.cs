namespace CurrencyExchange.Contracts.ExchangeRate.DTOs;

public record CreateExchangeRateDTO(Guid BaseCurrencyId, Guid TargetCurrencyId, decimal Rate);