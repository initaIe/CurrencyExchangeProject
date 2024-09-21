namespace CurrencyExchange.Domain.Helpers;

public static class ExchangeCalculator
{
    public static decimal Calculate(decimal amount, decimal rate)
    {
        return amount * rate;
    }
}