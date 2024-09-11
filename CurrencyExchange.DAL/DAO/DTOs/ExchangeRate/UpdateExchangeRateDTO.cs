namespace CurrencyExchange.DAL.DAO.DTOs.ExchangeRate;

public class UpdateExchangeRateDTO
{
    public int Id { get; set; }
    public int BaseCurrencyId { get; set; }
    public int TargetCurrencyId { get; set; }
    public decimal Rate { get; set; }
}