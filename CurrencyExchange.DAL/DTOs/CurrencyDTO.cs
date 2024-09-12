namespace CurrencyExchange.DAL.DTOs;

public class CurrencyDTO
{
    public Guid Id { get; set; } 
    public string Code { get; set; } 
    public string FullName { get; set; } 
    public string Sign { get; set; } 
}