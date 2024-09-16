using CurrencyExchange.Contracts.Currency;
using CurrencyExchange.Domain.Response.Interfaces;

namespace CurrencyExchange.Service.Interfaces;

public interface ICurrencyService
{
    Task<IResponse> CreateAsync(CreateCurrencyDTO dto);
    Task<IResponse> GetAllAsync(int pageSize, int pageNumber);
    Task<IResponse> GetByIdAsync(Guid id);
    Task<IResponse> UpdateAsync(Guid id, UpdateCurrencyDTO dto);
    Task<IResponse> DeleteAsync(Guid id);
}