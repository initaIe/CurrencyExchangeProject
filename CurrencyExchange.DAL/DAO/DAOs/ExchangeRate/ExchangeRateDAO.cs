namespace CurrencyExchange.DAL.DAO.DTOs.ExchangeRate;

public class ExchangeRateDAO
{
    public Guid Id { get; set; }
    public int BaseCurrencyId { get; set; }
    public int TargetCurrencyId { get; set; }
    public decimal Rate { get; set; }
}