using CurrencyExchange.Domain.Result;

namespace CurrencyExchange.DAL.Repository.Interfaces;

public interface IRepository<TDomainModel, TEntity>
{
    Task<IResult<TDomainModel>> CreateAsync(TDomainModel model);
    Task<IResult<TEntity>> GetByIdAsync(Guid id);
    Task<IResult<IEnumerable<TEntity>>> GetAllAsync(int limit, int offset);
    Task<IResult<Guid>> DeleteAsync(Guid id);
    Task<IResult<TDomainModel>> UpdateAsync(Guid id, TDomainModel model);
}