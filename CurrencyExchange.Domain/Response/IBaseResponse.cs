using CurrencyExchange.Domain.Enum;

namespace CurrencyExchange.Domain.Response;

public interface IBaseResponse<T>
{
    string Description { get; }
    StatusCode StatusCode { get; }
    T Data { get; }
}