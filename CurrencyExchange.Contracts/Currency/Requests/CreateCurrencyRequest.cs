namespace CurrencyExchange.Contracts.Currency.Requests;

public record CreateCurrencyRequest(string Code, string FullName, string Sign);