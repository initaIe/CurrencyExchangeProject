using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Response.Interfaces;

namespace CurrencyExchange.Service.Interfaces;

public interface IExchangeRateService
{
    Task<IResponse> GetCurrenciesAsync(int pageSize = 0, int pageNumber = 0);
    Task<IResponse> GetCurrencyByIdAsync(Guid id);
    Task<IResponse> CreateCurrencyAsync(ExchangeRate exchangeRate);
    Task<IResponse> UpdateCurrencyAsync(Guid id, ExchangeRate exchangeRate);
    Task<IResponse> DeleteCurrencyAsync(Guid id);
}