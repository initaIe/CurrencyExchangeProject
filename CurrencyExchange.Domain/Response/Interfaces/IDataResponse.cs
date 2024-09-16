namespace CurrencyExchange.Domain.Response.Interfaces;

public interface IDataResponse<T> : IResponse
{
    T Data { get; }
}