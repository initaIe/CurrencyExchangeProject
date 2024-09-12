using CurrencyExchange.Domain.Entities;

namespace CurrencyExchange.Domain.Entity;

public class ExchangeRate
{
    public int Id { get; set; }
    public Currency BaseCurrency { get; set; } = default!;
    public Currency TargetCurrency { get; set; } = default!;
    public decimal Rate { get; set; }
}