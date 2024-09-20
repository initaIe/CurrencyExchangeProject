using CurrencyExchange.Domain.Result.Interfaces;

namespace CurrencyExchange.Domain.Result.Implementations;

public class Result<T> : IResult<T>
{
    // TODO: rework response + Result classes
    private Result(bool isSuccess, T? data, List<string>? errors, string? error)
    {
        IsSuccess = isSuccess;
        Data = data ?? default;

        if (errors is { Count: > 0 })
            Errors = [..errors];
        else if (!string.IsNullOrWhiteSpace(error))
            Errors = [error];
        else
            Errors = default;
    }

    public bool IsSuccess { get; }
    public T? Data { get; }
    public List<string>? Errors { get; }

    public static Result<T> Success()
    {
        return new Result<T>(true, default, null, null);
    }

    public static Result<T> Success(T data)
    {
        return new Result<T>(true, data, null, null);
    }

    public static Result<T> Failure()
    {
        return new Result<T>(false, default, null, null);
    }

    public static Result<T> Failure(List<string> errors)
    {
        return new Result<T>(false, default, errors, null);
    }

    public static Result<T> Failure(string error)
    {
        return new Result<T>(false, default, null, error);
    }
}