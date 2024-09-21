using System.ComponentModel.DataAnnotations;
using CurrencyExchange.Domain.Models;

namespace CurrencyExchange.Contracts.CurrencyContracts.Requests;

public record UpdateCurrencyRequest(
    [Required]
    [MinLength(Currency.CodeLength)]
    [MaxLength(Currency.CodeLength)]
    string Code,
    [Required]
    [MinLength(Currency.MinFullNameLength)]
    [MaxLength(Currency.MaxFullNameLength)]
    string FullName,
    [Required]
    [MinLength(Currency.MinSignLength)]
    [MaxLength(Currency.MaxSignLength)]
    string Sign);