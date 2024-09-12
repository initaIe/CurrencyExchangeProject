namespace CurrencyExchange.Domain.Entities;

public class Currency(Guid id, string code, string fullName, string sign)
{
    public const int MaxCodeLength = 3;
    public const int MaxFullNameLength = 50;
    public const int MaxSignLength = 10;

    public Guid Id { get; } = id;
    public string Code { get; } = code;
    public string FullName { get; } = fullName;
    public string Sign { get; } = sign;

    public static (Currency currency, string error) Create(Guid id, string code, string fullName, string sign)
    {
        var error = string.Empty;

        // TODO: write extension for Guid validation
        if (Guid.Empty.Equals(id) || string.IsNullOrEmpty(id.ToString()))
            error += "Id cannot be null or empty. ";

        if (string.IsNullOrEmpty(code) || code.Length > MaxCodeLength)
            error += "Code cannot be null, empty or larger than 3 symbols. ";

        if (string.IsNullOrEmpty(fullName) || fullName.Length > MaxFullNameLength)
            error += "FullName cannot be null, empty or larger than 50 symbols. ";

        if (string.IsNullOrEmpty(sign) || sign.Length > MaxSignLength)
            error += "Sign cannot be null, empty or larger than 10 symbols. ";

        var currency = new Currency(id, code, fullName, sign);

        return (currency, error);
    }
}