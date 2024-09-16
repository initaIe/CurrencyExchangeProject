using CurrencyExchange.Domain.Response.Interfaces;

namespace CurrencyExchange.Domain.Response.Implementations;

public class SuccessDataResponse<T>(string status, string message, int statusCode, T data)
    : IDataResponse<T>
{
    public string Status { get; } = status;
    public string Message { get; } = message;
    public int StatusCode { get; } = statusCode;
    public T Data { get; } = data;
}