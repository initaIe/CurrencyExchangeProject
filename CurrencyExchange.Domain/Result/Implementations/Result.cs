using CurrencyExchange.Domain.Result.Interfaces;

namespace CurrencyExchange.Domain.Result.Implementations;

public class Result<T> : IResult<T>
{
    private Result(T? data)
    {
        Data = data;
    }
        
    public bool IsSuccess => Data != null; 
    public T? Data { get; }
    
    public static Result<T> Success(T data)
    {
        return new Result<T>(data);
    }

    public static Result<T> Failure()
    {
        return new Result<T>(default);
    }
}