using System.ComponentModel.DataAnnotations;

namespace CurrencyExchange.Contracts.ExchangeRateContracts.Requests;

public record UpdateExchangeRateRequest(
    [Required] Guid BaseCurrencyId,
    [Required] Guid TargetCurrencyId,
    [Required] decimal Rate);