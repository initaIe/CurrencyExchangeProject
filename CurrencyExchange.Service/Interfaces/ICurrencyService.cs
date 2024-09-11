using CurrencyExchange.Domain.Response;
using CurrencyExchange.DTOs.Currency;

namespace CurrencyExchange.Service.Interfaces;

public interface ICurrencyService
{
    Task<BaseResponse<IEnumerable<GetCurrencyDTO>>> GetCurrenciesAsync();
    Task<BaseResponse<IEnumerable<GetCurrencyDTO>>> GetCurrenciesAsync(int pageSize, int pageNumber);
    Task<BaseResponse<GetCurrencyDTO>> GetCurrencyByIdAsync(int id);
    Task<BaseResponse<bool>> CreateCurrencyAsync(CreateCurrencyDTO dto);
    Task<BaseResponse<bool>> UpdateCurrencyAsync(UpdateCurrencyDTO dto);
    Task<BaseResponse<bool>> DeleteCurrencyAsync(int id);
}