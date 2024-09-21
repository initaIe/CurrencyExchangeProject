namespace CurrencyExchange.Contracts.ExchangeRateContracts.DTOs;

public record UpdateExchangeRateDTO(Guid BaseCurrencyId, Guid TargetCurrencyId, decimal Rate);