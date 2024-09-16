namespace CurrencyExchange.Domain.Models;

public class Currency
{
    public const int MaxCodeLength = 3;
    public const int MaxFullNameLength = 50;
    public const int MaxSignLength = 10;

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

    public static (Currency? currency, List<string> errors) Create(Guid id, string code, string fullName, string sign)
    {
        List<string> errors = [];

        if (Guid.Empty.Equals(id))
            errors.Add("Guid Id cannot be empty");
        
        if (string.IsNullOrEmpty(code) || code.Length > MaxCodeLength)
            errors.Add("Code cannot be null, empty or larger than 3 symbols");

        if (string.IsNullOrEmpty(fullName) || fullName.Length > MaxFullNameLength)
            errors.Add("FullName cannot be null, empty or larger than 50 symbols");

        if (string.IsNullOrEmpty(sign) || sign.Length > MaxSignLength)
            errors.Add("Sign cannot be null, empty or larger than 10 symbols");

        if (errors.Count > 0) return (null, errors);

        var currency = new Currency(id, code, fullName, sign);

        return (currency, errors);
    }
}