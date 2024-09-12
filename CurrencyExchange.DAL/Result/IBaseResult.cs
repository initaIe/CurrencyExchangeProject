namespace CurrencyExchange.DAL.Result;

public interface IBaseResult<T>
{
    bool IsSuccess { get; }
    string? Message { get; }
    T? Data { get; }
}