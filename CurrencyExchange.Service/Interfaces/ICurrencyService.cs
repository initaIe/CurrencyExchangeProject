using CurrencyExchange.Contracts.Currency.DTOs;
using CurrencyExchange.Domain.Response.Interfaces;

namespace CurrencyExchange.Service.Interfaces;

public interface ICurrencyService
{
    Task<IBaseResponse<CurrencyDTO>> CreateAsync(CreateCurrencyDTO dto);
    Task<IBaseResponse<IEnumerable<CurrencyDTO>>> GetAllAsync(int pageSize, int pageNumber);
    Task<IBaseResponse<CurrencyDTO>> GetByIdAsync(Guid id);
    Task<IBaseResponse<CurrencyDTO>> GetByCodeAsync(string code);
    Task<IBaseResponse<CurrencyDTO>> UpdateAsync(Guid id, UpdateCurrencyDTO dto);
    Task<IBaseResponse<Guid>> DeleteAsync(Guid id);
}