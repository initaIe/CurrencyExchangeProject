using CurrencyExchange.Domain.Enums;

namespace CurrencyExchange.Domain.Response;

public class BaseResponse<T> : IBaseResponse<T>
{
    public MessageText? Message { get; set; }
    public StatusCode? StatusCode { get; set; }
    public T? Data { get; set; }
}