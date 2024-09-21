namespace CurrencyExchange.Contracts.ExchangeContracts.DTOs;

public record GetExchangeDTO(
    string FromCode,
    string ToCode,
    decimal Amount);