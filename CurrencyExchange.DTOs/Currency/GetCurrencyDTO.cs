namespace CurrencyExchange.DTOs.Currency;

public class GetCurrencyDTO
{
    public int Id { get; set; }
    public string Code { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Sign { get; set; } = default!;
}