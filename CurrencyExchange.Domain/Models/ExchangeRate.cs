using CurrencyExchange.Domain.Helpers;

namespace CurrencyExchange.Domain.Models;

public class ExchangeRate
{
    public const int MaxDecimalPrecision = 6;

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

    public static (ExchangeRate? exchangeRate, List<string> errors) Create(Guid id, Currency baseCurrency,
        Currency targetCurrency, decimal rate)
    {
        List<string> errors = [];

        // TODO: write extension for Guid validation
        if (Guid.Empty.Equals(id))
            errors.Add("Id cannot be null or empty");

        if (!DecimalHelper.HasValidDecimalPrecision(rate, MaxDecimalPrecision))
            errors.Add("Rate must have maximum 6 numbers after comma");

        if (errors.Count > 0) return (null, errors);

        var exchangeRate = new ExchangeRate(id, baseCurrency, targetCurrency, rate);

        return (exchangeRate, errors);
    }
}