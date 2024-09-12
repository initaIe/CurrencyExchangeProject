namespace CurrencyExchange.DAL.DAO.DTOs.Currency;

public record CurrencyDAO
{
    public Guid Id { get; set; }
    public string Code { get; set; } 
    public string FullName { get; set; } 
    public string Sign { get; set; } 
}