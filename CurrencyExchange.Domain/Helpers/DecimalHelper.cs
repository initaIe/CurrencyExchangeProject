namespace CurrencyExchange.Domain.Helpers;

public static class DecimalHelper
{
    public static int GetCountNumbersAfterComma(decimal value)
    {
        return BitConverter.GetBytes(decimal.GetBits(value)[3])[2];
    }

    public static bool HasValidDecimalPrecision(decimal value, int decimalPrecision)
    {
        return GetCountNumbersAfterComma(value) <= decimalPrecision;
    }
}