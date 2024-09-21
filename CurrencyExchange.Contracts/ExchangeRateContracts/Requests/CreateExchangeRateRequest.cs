using System.ComponentModel.DataAnnotations;

namespace CurrencyExchange.Contracts.ExchangeRateContracts.Requests;

public record CreateExchangeRateRequest(
    [Required] Guid BaseCurrencyId,
    [Required] Guid TargetCurrencyId,
    [Required] decimal Rate);