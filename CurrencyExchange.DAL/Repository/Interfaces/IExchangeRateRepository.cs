using CurrencyExchange.DAL.Entities;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Result;

namespace CurrencyExchange.DAL.Repository.Interfaces;

public interface IExchangeRateRepository
{
    Task<IResult<ExchangeRate>> Create(ExchangeRate exchangeRate);
    Task<IResult<ExchangeRateEntity>> GetById(Guid id);
    Task<IResult<IEnumerable<ExchangeRateEntity>>> GetAll(int limit, int offset);
    Task<IResult<Guid>> Delete(Guid id);
    Task<IResult<ExchangeRate>> Update(ExchangeRate exchangeRate);
}