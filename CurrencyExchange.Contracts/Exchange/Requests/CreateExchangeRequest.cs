using System.ComponentModel.DataAnnotations;

namespace CurrencyExchange.Contracts.Exchange.Requests;

public record CreateExchangeRequest(
    [Required] string From,
    [Required] string To,
    [Required] int Amount);