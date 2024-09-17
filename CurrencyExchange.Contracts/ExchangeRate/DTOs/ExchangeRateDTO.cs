using CurrencyExchange.Contracts.Currency.DTOs;

namespace CurrencyExchange.Contracts.ExchangeRate.DTOs;

public record ExchangeRateDTO(Guid Id, CurrencyDTO BaseCurrency, CurrencyDTO TargetCurrency, decimal Rate);