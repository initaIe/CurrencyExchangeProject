namespace CurrencyExchange.Contracts.Exchange.DTOs;

public record CreateExchangeDTO(
    string FromCode,
    string ToCode,
    decimal Amount);