namespace CurrencyExchange.DTOs.ExchangeRate;

public class CreateExchangeRateDTO
{
    public int BaseCurrencyId { get; set; } 
    public int TargetCurrencyId { get; set; } 
    public decimal Rate { get; set; }
}