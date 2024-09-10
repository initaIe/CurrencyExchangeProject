namespace CurrencyExchange.DTOs.Currency;

public class CreateCurrencyDTO
{
    public string Code { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Sign { get; set; } = null!;
}