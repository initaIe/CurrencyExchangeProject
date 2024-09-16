namespace CurrencyExchange.DAL.Entities;

public class CurrencyEntity
{
    // TODO: change string ID on Guid ID
    public string Id { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Sign { get; set; } = default!;
}