using CurrencyExchange.Domain.Response;
using CurrencyExchange.Service.DTOs;

namespace CurrencyExchange.Service.Interfaces;

public interface ICurrencyService
{
    Task<BaseResponse<IEnumerable<CurrencyDTO>>> GetCurrenciesAsync();
    Task<BaseResponse<IEnumerable<CurrencyDTO>>> GetCurrenciesAsync(int pageSize, int pageNumber);
    Task<BaseResponse<CurrencyDTO>> GetCurrencyByIdAsync(int id);
    Task<BaseResponse<bool>> CreateCurrencyAsync(CurrencyDTO dto);
    Task<BaseResponse<bool>> UpdateCurrencyAsync(int id, CurrencyDTO dto);
    Task<BaseResponse<bool>> DeleteCurrencyAsync(int id);
}