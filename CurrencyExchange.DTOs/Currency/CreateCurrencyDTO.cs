namespace CurrencyExchange.DTOs.Currency;

public class CreateCurrencyDTO
{
    public string Code { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Sign { get; set; } = default!;
}