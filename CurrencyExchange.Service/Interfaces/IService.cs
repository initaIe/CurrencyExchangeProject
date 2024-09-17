using CurrencyExchange.Domain.Response.Interfaces;

namespace CurrencyExchange.Service.Interfaces;

public interface IService<TCreateDTO, TUpdateDTO>
{
    Task<IResponse> CreateAsync(TCreateDTO dto);
    Task<IResponse> GetAllAsync(int pageSize, int pageNumber);
    Task<IResponse> GetByIdAsync(Guid id);
    Task<IResponse> UpdateAsync(Guid id, TUpdateDTO dto);
    Task<IResponse> DeleteAsync(Guid id);
}