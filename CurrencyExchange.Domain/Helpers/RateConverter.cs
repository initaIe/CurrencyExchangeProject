namespace CurrencyExchange.Domain.Helpers;

public static class RateConverter
{
    public static decimal GetFromReverseRate(decimal reverseRate)
    {
        return 1m / reverseRate;
    }

    public static decimal GetFromCrossRates(decimal currentRateToBaseRate, decimal targetRateToBaseRate)
    {
        return currentRateToBaseRate / targetRateToBaseRate;
    }
}