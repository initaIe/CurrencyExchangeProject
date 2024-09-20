using CurrencyExchange.Domain.Result.Implementations;

namespace CurrencyExchange.Domain.Helpers;

public static class StringValidator
{
    public static Result<string> MinMaxLength(string input, int minLength, int maxLength)
    {
        List<string> errors = [];

        if (input.Length < minLength) errors.Add($"Length must be larger than or equal {minLength}.");

        if (input.Length > maxLength) errors.Add($"Length must be smaller than or equal {maxLength}.");

        return errors.Count > 0 ? Result<string>.Failure(errors) : Result<string>.Success();
    }

    public static Result<string> Length(string input, int length)
    {
        if (input.Length != length)
        {
            var error = $"Length must be equal to {length}.";
            return Result<string>.Failure(error);
        }

        return Result<string>.Success();
    }

    public static Result<string> NullEmpty(string? input)
    {
        List<string> errors = [];

        if (input == null) errors.Add("Cannot be null.");

        if (string.IsNullOrEmpty(input)) errors.Add("Cannot be empty.");

        return errors.Count > 0 ? Result<string>.Failure(errors) : Result<string>.Success();
    }

    public static Result<string> WhiteSpaces(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return Result<string>.Failure("Cannot consist only of whitespace.");

        return Result<string>.Success();
    }

    public static Result<string> Case(string input, bool isMustBeUpperCase)
    {
        string error;

        if (isMustBeUpperCase)
        {
            if (input != input.ToUpper())
            {
                error = "Input must be in uppercase.";
                return Result<string>.Failure(error);
            }
        }
        else
        {
            if (input != input.ToLower())
            {
                error = "Input must be in lowercase.";
                return Result<string>.Failure(error);
            }
        }

        return Result<string>.Success();
    }
}