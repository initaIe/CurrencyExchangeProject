using CurrencyExchange.Contracts.CurrencyContracts.DTOs;

namespace CurrencyExchange.Contracts.ExchangeRateContracts.DTOs;

public record ExchangeRateDTO(Guid Id, CurrencyDTO BaseCurrency, CurrencyDTO TargetCurrency, decimal Rate);