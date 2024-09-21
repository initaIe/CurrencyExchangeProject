namespace CurrencyExchange.Contracts.ExchangeRateContracts.DTOs;

public record CreateExchangeRateDTO(Guid BaseCurrencyId, Guid TargetCurrencyId, decimal Rate);