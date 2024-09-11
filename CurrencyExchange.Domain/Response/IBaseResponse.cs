using CurrencyExchange.Domain.Enums;

namespace CurrencyExchange.Domain.Response;

public interface IBaseResponse<T>
{
    string Description { get; }
    StatusCode StatusCode { get; }
    T Data { get; }
}