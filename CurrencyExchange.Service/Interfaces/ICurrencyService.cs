using CurrencyExchange.Contracts.Currency;
using CurrencyExchange.Domain.Response;

namespace CurrencyExchange.Service.Interfaces;

public interface ICurrencyService
{
    Task<IBaseResponse<CurrencyDTO>> CreateCurrencyAsync(CreateCurrencyDTO dto);
    Task<IBaseResponse<IEnumerable<CurrencyDTO>>> GetCurrenciesAsync(int pageSize, int pageNumber);
    Task<IBaseResponse<CurrencyDTO>> GetCurrencyByIdAsync(Guid id);
    Task<IBaseResponse<CurrencyDTO>> UpdateCurrencyAsync(UpdateCurrencyDTO dto);
    Task<IBaseResponse<Guid>> DeleteCurrencyAsync(Guid id);
}