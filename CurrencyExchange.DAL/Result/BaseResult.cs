namespace CurrencyExchange.DAL.Result;

public class BaseResult<T> : IBaseResult<T>
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
}