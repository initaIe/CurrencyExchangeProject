namespace CurrencyExchange.Domain.Entities;

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

    public static (Currency? currency, string error) Create(Guid id, string code, string fullName, string sign)
    {
        var error = string.Empty;

        // TODO: write extension for Guid validation
        if (Guid.Empty.Equals(id))
            error += "Id cannot be null or empty. ";

        if (string.IsNullOrEmpty(code) || code.Length > MaxCodeLength)
            error += "Code cannot be null, empty or larger than 3 symbols. ";

        if (string.IsNullOrEmpty(fullName) || fullName.Length > MaxFullNameLength)
            error += "FullName cannot be null, empty or larger than 50 symbols. ";

        if (string.IsNullOrEmpty(sign) || sign.Length > MaxSignLength)
            error += "Sign cannot be null, empty or larger than 10 symbols. ";

        if (!string.IsNullOrEmpty(error)) return (null, error);
        
        var currency = new Currency(id, code, fullName, sign);
        
        return (currency, error);
    }
}