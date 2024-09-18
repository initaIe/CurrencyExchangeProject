using CurrencyExchange.Domain.Enums;

namespace CurrencyExchange.Domain.Response.Interfaces;

public interface IBaseResponse<T>
{
    string? Message { get; }
    StatusCode StatusCode { get; }
    T? Data { get; }
    List<string> Errors { get; }
}