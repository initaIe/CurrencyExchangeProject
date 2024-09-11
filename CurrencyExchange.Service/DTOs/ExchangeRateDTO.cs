namespace CurrencyExchange.Service.DTOs;

public class ExchangeRateDTO
{
    public int Id { get; set; }
    public CurrencyDTO BaseCurrency { get; set; } = default!;
    public CurrencyDTO TargetCurrency { get; set; } = default!;
    public decimal Rate { get; set; }
}