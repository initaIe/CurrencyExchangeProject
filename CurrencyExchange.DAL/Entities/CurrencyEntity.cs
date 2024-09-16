namespace CurrencyExchange.DAL.Entities;

public class CurrencyEntity
{
    public string Id { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Sign { get; set; } = default!;
}