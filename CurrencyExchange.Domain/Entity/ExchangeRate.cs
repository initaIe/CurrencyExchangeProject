namespace CurrencyExchange.Domain.Entity;

public class ExchangeRate
{
    public int Id { get; set; }
    public Currency BaseCurrencyId { get; set; } = null!;
    public Currency TargetCurrencyId { get; set; } = null!;
    public decimal Rate { get; set; }
}