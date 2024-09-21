using System.ComponentModel.DataAnnotations;
using CurrencyExchange.Domain.Models;

namespace CurrencyExchange.Contracts.ExchangeRateContracts.Requests;

public record GetByCodesRequest(
    [Required]
    [MinLength(Currency.CodeLength)]
    [MaxLength(Currency.CodeLength)]
    string BaseCurrencyCode,
    [Required]
    [MinLength(Currency.CodeLength)]
    [MaxLength(Currency.CodeLength)]
    string TargetCurrencyCode);