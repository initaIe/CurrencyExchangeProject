using CurrencyExchange.Domain.Result.Implementations;

namespace CurrencyExchange.Domain.Helpers;

public static class GuidValidator
{
    public static Result<Guid> Validate(string input)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(input)) errors.Add("GUID string cannot be null, empty, or whitespace.");

        if (!Guid.TryParse(input, out var guid)) errors.Add("Invalid GUID format.");

        if (errors.Count > 0) return Result<Guid>.Failure(errors);

        return Result<Guid>.Success();
    }

    public static Result<Guid> Validate(Guid input)
    {
        if (Guid.Empty.Equals(input))
        {
            var error = "GUID cannot be empty.";
            return Result<Guid>.Failure(error);
        }

        return Result<Guid>.Success();
    }
}