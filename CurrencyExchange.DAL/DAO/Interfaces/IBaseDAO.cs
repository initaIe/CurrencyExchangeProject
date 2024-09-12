namespace CurrencyExchange.DAL.DAO.Interfaces;

public interface IBaseDAO<TDAOEntity>
{
    Task<bool> CreateAsync(TDAOEntity entity);
    Task<TDAOEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<TDAOEntity>> GetAllAsync();
    Task<IEnumerable<TDAOEntity>> GetAllAsync(int pageSize, int page);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> UpdateAsync(TDAOEntity entity);
}