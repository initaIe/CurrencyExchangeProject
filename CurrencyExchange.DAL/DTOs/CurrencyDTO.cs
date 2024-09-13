namespace CurrencyExchange.DAL.DTOs;

public class CurrencyDTO
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Sign { get; set; } = default!;
}