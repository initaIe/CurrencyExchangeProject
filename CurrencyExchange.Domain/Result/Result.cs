namespace CurrencyExchange.Domain.Result;

public record Result<T> : IResult<T>
{
    /// <summary>
    ///     Конструктор для успешного результата.
    /// </summary>
    public Result(T data)
    {
        IsSuccess = true;
        Data = data;
    }

    /// <summary>
    ///     Конструктор для неуспешного результата.
    /// </summary>
    public Result()
    {
        IsSuccess = false;
    }

    public bool IsSuccess { get; }
    public T? Data { get; }
}