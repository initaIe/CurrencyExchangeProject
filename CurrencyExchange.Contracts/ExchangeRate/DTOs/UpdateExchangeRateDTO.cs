using CurrencyExchange.Contracts.Currency.DTOs;

namespace CurrencyExchange.Contracts.ExchangeRate.DTOs;

public record UpdateExchangeRateDTO(Guid BaseCurrencyId, Guid TargetCurrencyId, decimal Rate);