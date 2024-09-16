using CurrencyExchange.Domain.Response.Interfaces;

namespace CurrencyExchange.Domain.Response.Implementations;

public class Response(string status, string message, int statusCode) : IResponse
{
    public string Status { get; } = status;
    public string Message { get; } = message;
    public int StatusCode { get; } = statusCode;
}