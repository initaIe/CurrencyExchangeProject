using CurrencyExchange.DAL.Entities;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Result;

namespace CurrencyExchange.DAL.Repository.Interfaces;

public interface IExchangeRateRepository
{
    Task<IBaseResult<ExchangeRate>> Create(ExchangeRate exchangeRate);
    Task<IBaseResult<ExchangeRateEntity>> GetById(Guid id);
    Task<IBaseResult<IEnumerable<ExchangeRateEntity>>> GetAll(int limit, int offset);
    Task<IBaseResult<Guid>> Delete(Guid id);
    Task<IBaseResult<ExchangeRate>> Update(ExchangeRate exchangeRate);
}