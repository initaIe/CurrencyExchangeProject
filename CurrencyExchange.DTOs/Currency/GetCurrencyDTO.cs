namespace CurrencyExchange.DTOs.Currency;

public class GetCurrencyDTO
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Sign { get; set; } = null!;
}