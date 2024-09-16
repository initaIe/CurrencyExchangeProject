using CurrencyExchange.Domain.Enums;

namespace CurrencyExchange.Domain.Response;

public interface IResponse
{
    string Status { get; }
    string Message { get; }
    StatusCode StatusCode { get; }
}