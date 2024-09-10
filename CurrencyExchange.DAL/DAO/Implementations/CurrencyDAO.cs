using CurrencyExchange.DAL.Interfaces;
using CurrencyExchange.Domain.Entity;

namespace CurrencyExchange.DAL.Implementations;

public class CurrencyDAO : IBaseDAO<Currency>
{
    public Task Create(Currency entity)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Currency> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Delete(Currency entity)
    {
        throw new NotImplementedException();
    }

    public Task<Currency> Update(Currency entity)
    {
        throw new NotImplementedException();
    }
}