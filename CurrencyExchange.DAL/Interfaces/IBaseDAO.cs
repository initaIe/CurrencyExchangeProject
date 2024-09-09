using System.Collections;
using CurrencyExchange.Domain.Entity;

namespace CurrencyExchange.DAL.Interfaces;

public interface IBaseDAO<T>
{
    Task Create(T entity);
    Task<T> Read(int id);
    Task<IEnumerable<T>> ReadAll();
    Task Delete(int id);
    Task Update(T entity);
}