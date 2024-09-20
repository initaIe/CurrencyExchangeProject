using CurrencyExchange.Domain.Result.Implementations;

namespace CurrencyExchange.Domain.Helpers;

public static class ResultHelper
{
    public static void AddErrorsIfNotSuccess<T>(Result<T> validationResult, List<string> errors)
    {
        if (validationResult is { IsSuccess: false, Errors: not null, Errors.Count: > 0 })
            errors.AddRange(validationResult.Errors);
    }
}