using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Result.Interfaces;

namespace CurrencyExchange.DAL.Repository.Interfaces;

public interface IExchangeRateRepository
{
    Task<IResult<ExchangeRate>> CreateAsync(ExchangeRate exchangeRate);
    Task<IResult<ExchangeRate>> GetByIdAsync(Guid id);
    Task<IResult<ExchangeRate>> GetByCodePairAsync(string fromCode, string toCode);
    Task<IResult<IEnumerable<ExchangeRate>>> GetAllAsync(int limit, int offset);
    Task<IResult<Guid>> DeleteAsync(Guid id);
    Task<IResult<ExchangeRate>> UpdateAsync(Guid id, ExchangeRate exchangeRate);
}