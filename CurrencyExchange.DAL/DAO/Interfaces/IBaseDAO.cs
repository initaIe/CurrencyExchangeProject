namespace CurrencyExchange.DAL.DAO.Interfaces;

public interface IBaseDAO<in TCreationDTO, TGetDTO, in TUpdateDTO>
{
    Task<bool> Create(TCreationDTO entity);
    Task<TGetDTO> GetById(int id);
    Task<IEnumerable<TGetDTO>> GetAll();
    Task<bool> Delete(int id);
    Task<bool> Update(TUpdateDTO entity);
}