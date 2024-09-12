using CurrencyExchange.DAL.Result;

namespace CurrencyExchange.DAL.DAO.Interfaces;

public interface IBaseDAO<T>
{
    Task<IBaseResult<T>> Create(T dto);
    Task<IBaseResult<T>> GetById(Guid id);
    Task<IBaseResult<IEnumerable<T>>> GetAll(int entitiesLimit, int entitiesOffset);
    Task<IBaseResult<T>> Delete(Guid id);
    Task<IBaseResult<T>> Update(Guid id, T dto);
}