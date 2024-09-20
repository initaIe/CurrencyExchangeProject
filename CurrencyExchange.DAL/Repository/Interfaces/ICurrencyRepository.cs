using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Result.Interfaces;

namespace CurrencyExchange.DAL.Repository.Interfaces;

public interface ICurrencyRepository
{
    Task<IResult<Currency>> CreateAsync(Currency currency);
    Task<IResult<Currency>> GetByIdAsync(Guid id);
    Task<IResult<Currency>> GetByCodeAsync(string code);
    Task<IResult<IEnumerable<Currency>>> GetAllAsync(int limit, int offset);
    Task<IResult<Guid>> DeleteAsync(Guid id);
    Task<IResult<Currency>> UpdateAsync(Guid id, Currency currency);
}