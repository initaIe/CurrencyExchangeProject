using CurrencyExchange.Domain.Helpers;
using CurrencyExchange.Domain.Result.Implementations;

namespace CurrencyExchange.Domain.Models;

public class Currency
{
    public const int CodeLength = 3;
    public const int MinFullNameLength = 1;
    public const int MaxFullNameLength = 30;
    public const int MinSignLength = 1;
    public const int MaxSignLength = 4;

    private Currency(Guid id, string code, string fullName, string sign)
    {
        Id = id;
        Code = code;
        FullName = fullName;
        Sign = sign;
    }

    public Guid Id { get; }
    public string Code { get; }
    public string FullName { get; }
    public string Sign { get; }

    // Используется для создания доменной модели при создании нового объекта, который требуется провалидировать
    public static Result<Currency> Create(Guid id, string code, string fullName, string sign)
    {
        List<string> errors = [];

        GuidValidator.Validate(id).AddErrorsIfNotSuccess(errors);

        StringValidator.Length(code, CodeLength).AddErrorsIfNotSuccess(errors);

        StringValidator.Case(code, true).AddErrorsIfNotSuccess(errors);

        StringValidator.MinMaxLength(fullName, MinFullNameLength, MaxFullNameLength).AddErrorsIfNotSuccess(errors);

        StringValidator.NullEmpty(fullName).AddErrorsIfNotSuccess(errors);

        StringValidator.MinMaxLength(sign, MinSignLength, MaxSignLength).AddErrorsIfNotSuccess(errors);

        StringValidator.NullEmpty(sign).AddErrorsIfNotSuccess(errors);

        if (errors.Count > 0) return Result<Currency>.Failure(errors);

        var currency = new Currency(id, code, fullName, sign);

        return Result<Currency>.Success(currency);
    }

    // Используется для создания доменной модели при получении из БД уже ВАЛИДНОГО ОБЪЕКТА
    public static Currency CreateWithoutValidation(Guid id, string code, string fullName, string sign)
    {
        return new Currency(id, code, fullName, sign);
    }
}