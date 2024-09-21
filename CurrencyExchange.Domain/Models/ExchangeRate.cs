using CurrencyExchange.Domain.Helpers;
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

    // Используется для создания доменной модели при создании нового объекта, который требуется провалидировать
    public static Result<ExchangeRate> Create(Guid id, Currency baseCurrency,
        Currency targetCurrency, decimal rate)
    {
        List<string> errors = [];

        var guidEmptyValidation = GuidValidator.Validate(id);
        guidEmptyValidation.AddErrorsIfNotSuccess(errors);

        if (!DecimalHelper.HasValidDecimalPrecision(rate, MaxDecimalPrecision))
            rate = Math.Round(rate, MaxDecimalPrecision);


        if (errors.Count > 0) return Result<ExchangeRate>.Failure(errors);

        var exchangeRate = new ExchangeRate(id, baseCurrency, targetCurrency, rate);

        return Result<ExchangeRate>.Success(exchangeRate);
    }

    // Используется для создания доменной модели при получении из БД уже ВАЛИДНОГО ОБЪЕКТА
    public static ExchangeRate CreateWithoutValidation(Guid id, Currency baseCurrency,
        Currency targetCurrency, decimal rate)
    {
        return new ExchangeRate(id, baseCurrency, targetCurrency, rate);
    }
}