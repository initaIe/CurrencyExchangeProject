namespace CurrencyExchange.Domain.Result.Interfaces;

public interface IResultWithErrors<T> : IResult<T>
{
    List<string> Errors { get; }
}