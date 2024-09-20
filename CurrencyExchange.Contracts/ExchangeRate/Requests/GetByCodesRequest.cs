using System.ComponentModel.DataAnnotations;

namespace CurrencyExchange.Contracts.ExchangeRate.Requests;

public record GetByCodesRequest([Required] string BaseCurrencyCode, [Required] string TargetCurrencyCode);