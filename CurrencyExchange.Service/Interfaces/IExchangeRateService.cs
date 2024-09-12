using CurrencyExchange.Domain.Response;
using CurrencyExchange.Service.DTOs;

namespace CurrencyExchange.Service.Interfaces;

public interface IExchangeRateService
{
    Task<BaseResponse<IEnumerable<ExchangeRateDTO>>> GetCurrenciesAsync();
    Task<BaseResponse<IEnumerable<ExchangeRateDTO>>> GetCurrenciesAsync(int pageSize, int pageNumber);
    Task<BaseResponse<ExchangeRateDTO>> GetCurrencyByIdAsync(int id);
    Task<BaseResponse<bool>> CreateCurrencyAsync(ExchangeRateDTO dto);
    Task<BaseResponse<bool>> UpdateCurrencyAsync(int id, ExchangeRateDTO dto);
    Task<BaseResponse<bool>> DeleteCurrencyAsync(int id);
}