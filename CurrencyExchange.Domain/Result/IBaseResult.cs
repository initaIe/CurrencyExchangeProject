using CurrencyExchange.Domain.Enums;

namespace CurrencyExchange.Domain.Result;

public interface IBaseResult<T>
{
    bool IsSuccess { get; }
    OperationStatus Status { get; }
    T? Data { get; }
}