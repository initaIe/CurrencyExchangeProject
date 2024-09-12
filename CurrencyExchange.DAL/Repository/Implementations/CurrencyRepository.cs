using CurrencyExchange.DAL.DAO.DTOs.Currency;
using CurrencyExchange.DAL.DAO.Interfaces;
using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Entities;

namespace CurrencyExchange.DAL.Repository.Implementations;

public class CurrencyRepository(IBaseDAO<CurrencyDAO, GetCurrencyDTO, UpdateCurrencyDTO> currencyDAO)
    : IBaseRepository<Currency>
{
    public async Task<bool> CreateAsync(Currency entity)
    {
        var dto = new CurrencyDAO
        {
            Code = entity.Code,
            FullName = entity.FullName,
            Sign = entity.Sign
        };
        return await currencyDAO.CreateAsync(dto);
    }

    public async Task<Currency?> GetByIdAsync(int id)
    {
        var dto = await currencyDAO.GetByIdAsync(id);

        if (dto == null)
            return null;

        var result = new Currency
        {
            Id = dto.Id,
            Code = dto.Code,
            FullName = dto.FullName,
            Sign = dto.Sign
        };

        return result;
    }

    public async Task<IEnumerable<Currency>> GetAllAsync()
    {
        var dtoList = await currencyDAO.GetAllAsync();

        return dtoList.Select(dto => new Currency
        {
            Id = dto.Id,
            Code = dto.Code,
            FullName = dto.FullName,
            Sign = dto.Sign
        });
    }

    public async Task<IEnumerable<Currency>> GetAllAsync(int pageSize, int pageNumber)
    {
        var dtoList = await currencyDAO.GetAllAsync(pageSize, pageNumber);

        return dtoList.Select(dto => new Currency
        {
            Id = dto.Id,
            Code = dto.Code,
            FullName = dto.FullName,
            Sign = dto.Sign
        });
    }

    public async Task<bool> DeleteAsync(Currency entity)
    {
        return await currencyDAO.DeleteAsync(entity.Id);
    }

    public async Task<bool> UpdateAsync(Currency entity)
    {
        var dto = new UpdateCurrencyDTO
        {
            Id = entity.Id,
            Code = entity.Code,
            FullName = entity.FullName,
            Sign = entity.Sign
        };
        return await currencyDAO.UpdateAsync(dto);
    }
}