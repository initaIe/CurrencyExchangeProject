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

        var guidEmptyValidation = GuidValidator.Validate(id);
        ResultHelper.AddErrorsIfNotSuccess(guidEmptyValidation, errors);

        var codeLengthValidation = StringValidator.Length(code, CodeLength);
        ResultHelper.AddErrorsIfNotSuccess(codeLengthValidation, errors);

        var codeCaseValidation = StringValidator.Case(code, true);
        ResultHelper.AddErrorsIfNotSuccess(codeCaseValidation, errors);

        var fullNameMinMaxValidation = StringValidator.MinMaxLength(fullName, MinFullNameLength, MaxFullNameLength);
        ResultHelper.AddErrorsIfNotSuccess(fullNameMinMaxValidation, errors);

        var fullNameNullEmptyValidation = StringValidator.NullEmpty(fullName);
        ResultHelper.AddErrorsIfNotSuccess(fullNameNullEmptyValidation, errors);

        var signMinMaxValidation = StringValidator.MinMaxLength(sign, MinSignLength, MaxSignLength);
        ResultHelper.AddErrorsIfNotSuccess(signMinMaxValidation, errors);

        var signNullEmptyValidation = StringValidator.NullEmpty(sign);
        ResultHelper.AddErrorsIfNotSuccess(signNullEmptyValidation, errors);

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