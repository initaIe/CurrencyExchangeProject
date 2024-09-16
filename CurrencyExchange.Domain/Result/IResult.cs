namespace CurrencyExchange.Domain.Result;

public interface IResult<T>
{
    bool IsSuccess { get; }
    T? Data { get; }
}