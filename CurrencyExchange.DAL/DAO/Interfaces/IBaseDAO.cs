using System.Collections;
using CurrencyExchange.Domain.Entity;

namespace CurrencyExchange.DAL.Interfaces;

public interface IBaseDAO<in TCreationDTO, TReadDTO, in TUpdateDTO>
{
    Task Create(TCreationDTO entity);
    Task<TReadDTO> GetById(int id);
    Task<IEnumerable<TReadDTO>> GetAll();
    Task Delete(int id);
    Task Update(TUpdateDTO entity);
}