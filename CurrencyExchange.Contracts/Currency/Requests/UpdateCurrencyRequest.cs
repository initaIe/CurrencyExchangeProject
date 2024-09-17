namespace CurrencyExchange.Contracts.Currency.Requests;

public record UpdateCurrencyRequest(string Code, string FullName, string Sign);