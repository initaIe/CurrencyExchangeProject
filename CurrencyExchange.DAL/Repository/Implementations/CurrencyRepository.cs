using CurrencyExchange.DAL.DAO.Interfaces;
using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Entity;
using CurrencyExchange.DTOs.Currency;

namespace CurrencyExchange.DAL.Repository.Implementations;

public class CurrencyRepository(IBaseDAO<CreateCurrencyDTO, GetCurrencyDTO, UpdateCurrencyDTO> currencyDAO)
    : IBaseRepository<Currency>
{
    public async Task<bool> Create(Currency entity)
    {
        var dto = new CreateCurrencyDTO
        {
            Code = entity.Code,
            FullName = entity.FullName,
            Sign = entity.Sign
        };
        return await currencyDAO.Create(dto);
    }

    public async Task<Currency> GetById(int id)
    {
        var dto = await currencyDAO.GetById(id);

        var result = new Currency
        {
            Id = dto.Id,
            Code = dto.Code,
            FullName = dto.FullName,
            Sign = dto.Sign
        };

        return result;
    }

    public async Task<IEnumerable<Currency>> GetAll()
    {
        var dtoList = await currencyDAO.GetAll();

        var result = dtoList.Select(dto => new Currency
        {
            Id = dto.Id,
            Code = dto.Code,
            FullName = dto.FullName,
            Sign = dto.Sign
        });

        return result;
    }

    public async Task<bool> Delete(Currency entity)
    {
        return await currencyDAO.Delete(entity.Id);
    }

    public async Task<bool> Update(Currency entity)
    {
        var dto = new UpdateCurrencyDTO
        {
            Id = entity.Id,
            Code = entity.Code,
            FullName = entity.FullName,
            Sign = entity.Sign
        };
        return await currencyDAO.Update(dto);
    }
}