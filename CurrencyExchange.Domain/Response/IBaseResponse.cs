using CurrencyExchange.Domain.Enums;

namespace CurrencyExchange.Domain.Response;

public interface IBaseResponse<T>
{
    bool IsSuccess { get; }
    string Status { get; }
    string Message { get; }
    StatusCode StatusCode { get; }
    T? Data { get; }
    string[]? Errors { get; }
}