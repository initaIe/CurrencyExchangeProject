using CurrencyExchange.Domain.Helpers;

namespace CurrencyExchange.Domain.Models;

public class ExchangeRate
{
    public const int DecimalPrecision = 6;

    private ExchangeRate(Guid id, Currency baseCurrency, Currency targetCurrency, decimal rate)
    {
        Id = id;
        BaseCurrency = baseCurrency;
        TargetCurrency = targetCurrency;
        Rate = rate;
    }

    public Guid Id { get; set; }
    public Currency BaseCurrency { get; set; }
    public Currency TargetCurrency { get; set; }
    public decimal Rate { get; set; }

    public static (ExchangeRate? exchangeRate, string error) Create(Guid id, Currency baseCurrency,
        Currency targetCurrency, decimal rate)
    {
        var error = string.Empty;

        // TODO: write extension for Guid validation
        if (Guid.Empty.Equals(id))
            error += "Id cannot be null or empty. ";

        if (DecimalHelper.HasValidDecimalPrecision(rate, DecimalPrecision))
            error += "Rate must have 6 numbers after comma";

        if (!string.IsNullOrEmpty(error)) return (null, error);

        var exchangeRate = new ExchangeRate(id, baseCurrency, targetCurrency, rate);

        return (exchangeRate, error);
    }
}