namespace CurrencyExchange.DAL.DTOs;

public class ExchangeRateDTO
{
    public int Id { get; set; }
    public CurrencyDTO BaseCurrency { get; set; } 
    public CurrencyDTO TargetCurrency { get; set; } 
    public decimal Rate { get; set; }
}