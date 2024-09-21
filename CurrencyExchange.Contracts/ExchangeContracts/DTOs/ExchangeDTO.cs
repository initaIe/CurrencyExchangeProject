using CurrencyExchange.Contracts.CurrencyContracts.DTOs;

namespace CurrencyExchange.Contracts.ExchangeContracts.DTOs;

public record ExchangeDTO(
    CurrencyDTO BaseCurrency,
    CurrencyDTO TargetCurrency,
    decimal Rate,
    decimal Amount,
    decimal ConvertedAmount);