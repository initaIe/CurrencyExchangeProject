namespace CurrencyExchange.Contracts.Currency;

public record UpdateCurrencyRequest(string Code, string FullName, string Sign);