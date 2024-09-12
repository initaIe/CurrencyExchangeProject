using CurrencyExchange.Domain.Enums;

namespace CurrencyExchange.Domain.Response;

public interface IBaseResponse<T>
{
    MessageText? Message { get; }
    StatusCode? StatusCode { get; }
    T? Data { get; }
}