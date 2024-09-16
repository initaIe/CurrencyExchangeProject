using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Response;
using CurrencyExchange.Service.DTOs;

namespace CurrencyExchange.Service.Interfaces;

public interface IExchangeRateService
{
    Task<IBaseResponse<IEnumerable<ExchangeRateDTO>>> GetCurrenciesAsync(int pageSize = 0, int pageNumber = 0);
    Task<IBaseResponse<ExchangeRateDTO>> GetCurrencyByIdAsync(Guid id);
    Task<IBaseResponse<ExchangeRate>> CreateCurrencyAsync(ExchangeRate exchangeRate);
    Task<IBaseResponse<ExchangeRate>> UpdateCurrencyAsync(Guid id, ExchangeRate exchangeRate);
    Task<IBaseResponse<Guid>> DeleteCurrencyAsync(Guid id);
}