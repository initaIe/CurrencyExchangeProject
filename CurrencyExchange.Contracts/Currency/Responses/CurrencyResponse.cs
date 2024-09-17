namespace CurrencyExchange.Contracts.Currency.Responses;

public record CurrencyResponse(Guid Id, string Code, string FullName, string Sign);