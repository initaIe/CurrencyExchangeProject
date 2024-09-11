namespace CurrencyExchange.DAL.DAO.Interfaces;

public interface IBaseDAO<in TCreationDTO, TGetDTO, in TUpdateDTO>
{
    Task<bool> CreateAsync(TCreationDTO entity);
    Task<TGetDTO> GetByIdAsync(int id);
    Task<IEnumerable<TGetDTO>> GetAllAsync();
    Task<IEnumerable<TGetDTO>> GetAllAsync(int pageSize, int page);
    Task<bool> DeleteAsync(int id);
    Task<bool> UpdateAsync(TUpdateDTO entity);
}