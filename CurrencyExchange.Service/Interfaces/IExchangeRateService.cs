using CurrencyExchange.DAL.DAO.DTOs.ExchangeRate;
using CurrencyExchange.Domain.Response;

namespace CurrencyExchange.Service.Interfaces;

public interface IExchangeRateService
{
    Task<BaseResponse<IEnumerable<GetExchangeRateDTO>>> GetExchangeRatesAsync();
    Task<BaseResponse<IEnumerable<GetExchangeRateDTO>>> GetExchangeRatesAsync(int pageSize, int pageNumber);
    Task<BaseResponse<GetExchangeRateDTO>> GetExchangeRateByIdAsync(int id);
    Task<BaseResponse<bool>> CreateExchangeRateAsync(CreateExchangeRateDTO dto);
    Task<BaseResponse<bool>> UpdateExchangeRateAsync(UpdateExchangeRateDTO dto);
    Task<BaseResponse<bool>> DeleteExchangeRateAsync(int id);
}