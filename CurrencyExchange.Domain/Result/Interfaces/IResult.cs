namespace CurrencyExchange.Domain.Result.Interfaces;

public interface IResult<T>
{
    bool IsSuccess { get; }
    T? Data { get; }
}