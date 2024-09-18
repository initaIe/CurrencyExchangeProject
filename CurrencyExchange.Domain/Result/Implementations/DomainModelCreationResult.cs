using CurrencyExchange.Domain.Result.Interfaces;

namespace CurrencyExchange.Domain.Result.Implementations;

public class DomainModelCreationResult<T> : IResultWithErrors<T>
{
    private DomainModelCreationResult(T? data, List<string> errors)
    {
        Data = data;
        Errors = errors ?? [];
    }
    
    public T? Data { get; }
    public bool IsSuccess => Errors.Count == 0;
    public List<string> Errors { get; }

    public static DomainModelCreationResult<T> Success(T entity)
    {
        return new DomainModelCreationResult<T>(entity, []);
    }

    public static DomainModelCreationResult<T> Failure(List<string> errors)
    {
        return new DomainModelCreationResult<T>(default, errors);
    }
}