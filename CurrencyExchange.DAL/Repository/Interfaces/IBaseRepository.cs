using CurrencyExchange.DAL.Result;

namespace CurrencyExchange.DAL.DAO.Interfaces;

public interface IBaseRepository<TDomainEntity, TEntityDTO>
{
    Task<IBaseResult<TDomainEntity>> Create(TEntityDTO dto);
    Task<IBaseResult<TDomainEntity>> GetById(Guid id);
    Task<IBaseResult<IEnumerable<TDomainEntity>>> GetAll(int limit, int offset);
    Task<IBaseResult<TDomainEntity>> Delete(Guid id);
    Task<IBaseResult<TDomainEntity>> Update(Guid id, TEntityDTO dto);
}