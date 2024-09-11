using System.Collections;

namespace CurrencyExchange.DAL.Repository.Interfaces;

public interface IBaseRepository<T>
{
    Task<bool> CreateAsync(T entity);
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(int pageSize, int pageNumber);
    Task<bool> DeleteAsync(T entity);
    Task<bool> UpdateAsync(T entity);
}