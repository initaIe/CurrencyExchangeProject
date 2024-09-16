using CurrencyExchange.DAL.Entities;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Result;

namespace CurrencyExchange.DAL.Repository.Interfaces;

public interface ICurrencyRepository
{
    Task<IBaseResult<Currency>> Create(Currency currency);
    Task<IBaseResult<CurrencyEntity>> GetById(Guid id);
    Task<IBaseResult<IEnumerable<CurrencyEntity>>> GetAll(int limit, int offset);
    Task<IBaseResult<Guid>> Delete(Guid id);
    Task<IBaseResult<Currency>> Update(Currency currency);
}