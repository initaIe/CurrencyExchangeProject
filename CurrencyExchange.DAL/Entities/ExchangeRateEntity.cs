namespace CurrencyExchange.DAL.Entities;

public class ExchangeRateEntity
{
    // TODO: change string ID on Guid ID
    public string Id { get; set; } = default!;
    public CurrencyEntity BaseCurrency { get; set; } = default!;
    public CurrencyEntity TargetCurrency { get; set; } = default!;
    public decimal Rate { get; set; }
}