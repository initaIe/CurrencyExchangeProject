namespace CurrencyExchange.DAL.DTOs;

public class CreateExchangeRateDTO
{
    public int Id { get; set; }
    public int BaseCurrencyId { get; set; } 
    public int TargetCurrencyId { get; set; } 
    public decimal Rate { get; set; }
}