namespace CurrencyExchange.Service.DTOs;

public class CurrencyDTO
{
    public int Id { get; set; }
    public string Code { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Sign { get; set; } = default!;
}