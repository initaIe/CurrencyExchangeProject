using CurrencyExchange.Domain.Helpers;
using CurrencyExchange.Domain.Result;
using CurrencyExchange.Domain.Result.Implementations;

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

    public static DomainModelCreationResult<ExchangeRate> Create(Guid id, Currency baseCurrency,
        Currency targetCurrency, decimal rate)
    {
        List<string> errors = [];

        if (Guid.Empty.Equals(id))
            errors.Add("Id cannot be null or empty.");

        if (!DecimalHelper.HasValidDecimalPrecision(rate, MaxDecimalPrecision))
            rate = Math.Round(rate, MaxDecimalPrecision);
        

        if (errors.Count > 0) return DomainModelCreationResult<ExchangeRate>.Failure(errors);

        var exchangeRate = new ExchangeRate(id, baseCurrency, targetCurrency, rate);

        return DomainModelCreationResult<ExchangeRate>.Success(exchangeRate);
    }
}