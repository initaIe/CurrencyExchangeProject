namespace CurrencyExchange.Contracts.Currency;

public record CreateCurrencyRequest(string Code, string FullName, string Sign);