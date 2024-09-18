using CurrencyExchange.Contracts.ExchangeRate.DTOs;
using CurrencyExchange.Domain.Response.Interfaces;

namespace CurrencyExchange.Service.Interfaces;

public interface IExchangeRateService
{
    Task<IBaseResponse<ExchangeRateDTO>> CreateAsync(CreateExchangeRateDTO dto);
    Task<IBaseResponse<IEnumerable<ExchangeRateDTO>>> GetAllAsync(int pageSize, int pageNumber);
    Task<IBaseResponse<ExchangeRateDTO>> GetByIdAsync(Guid id);
    Task<IBaseResponse<ExchangeRateDTO>> UpdateAsync(Guid id, UpdateExchangeRateDTO dto);
    Task<IBaseResponse<Guid>> DeleteAsync(Guid id);
}