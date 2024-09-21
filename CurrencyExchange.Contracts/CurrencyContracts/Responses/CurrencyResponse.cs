namespace CurrencyExchange.Contracts.CurrencyContracts.Responses;

public record CurrencyResponse(Guid Id, string Code, string FullName, string Sign);