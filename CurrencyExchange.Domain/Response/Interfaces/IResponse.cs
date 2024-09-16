namespace CurrencyExchange.Domain.Response.Interfaces;

public interface IResponse
{
    string Status { get; }
    string Message { get; }
    int StatusCode { get; }
}