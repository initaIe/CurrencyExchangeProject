namespace CurrencyExchange.Domain.Helpers;

public class ConversionCalculator
{
    public static decimal Convert(decimal amount, decimal rate)
    {
        return amount * rate;
    }
}