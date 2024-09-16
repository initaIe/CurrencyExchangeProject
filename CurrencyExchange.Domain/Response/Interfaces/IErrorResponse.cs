namespace CurrencyExchange.Domain.Response.Interfaces;

public interface IErrorResponse : IResponse
{
    List<string> Errors { get; }
}