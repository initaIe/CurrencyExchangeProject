using System.ComponentModel.DataAnnotations;
using CurrencyExchange.Domain.Models;

namespace CurrencyExchange.Contracts.ExchangeContracts.Requests;

public record GetExchangeRequest(
    [Required]
    [MinLength(Currency.CodeLength)]
    [MaxLength(Currency.CodeLength)]
    string From,
    [Required]
    [MinLength(Currency.CodeLength)]
    [MaxLength(Currency.CodeLength)]
    string To,
    [Required] int Amount);