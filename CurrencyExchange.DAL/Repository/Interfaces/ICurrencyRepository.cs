using CurrencyExchange.DAL.Entities;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Result;

namespace CurrencyExchange.DAL.Repository.Interfaces;

public interface ICurrencyRepository
{
    Task<IResult<Currency>> Create(Currency currency);
    Task<IResult<CurrencyEntity>> GetById(Guid id);
    Task<IResult<IEnumerable<CurrencyEntity>>> GetAll(int limit, int offset);
    Task<IResult<Guid>> Delete(Guid id);
    Task<IResult<Currency>> Update(Guid id, Currency currency);
}