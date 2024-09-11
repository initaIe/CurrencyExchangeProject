using CurrencyExchange.DAL.DAO.Interfaces;
using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Entity;
using CurrencyExchange.DTOs.ExchangeRate;

namespace CurrencyExchange.DAL.Repository.Implementations;

public class ExchangeRateRepository(
    IBaseDAO<CreateExchangeRateDTO, GetExchangeRateDTO, UpdateExchangeRateDTO> exchangeRateDAO,
    IBaseRepository<Currency> currencyRepository)
    : IBaseRepository<ExchangeRate>
{
    public async Task<bool> CreateAsync(ExchangeRate entity)
    {
        var dto = new CreateExchangeRateDTO
        {
            BaseCurrencyId = entity.BaseCurrency.Id,
            TargetCurrencyId = entity.TargetCurrency.Id,
            Rate = entity.Rate
        };
        return await exchangeRateDAO.CreateAsync(dto);
    }

    public async Task<ExchangeRate> GetByIdAsync(int id)
    {
        var dto = await exchangeRateDAO.GetByIdAsync(id);

        var baseCurrency = await currencyRepository.GetByIdAsync(dto.BaseCurrencyId);
        var targetCurrency = await currencyRepository.GetByIdAsync(dto.TargetCurrencyId);

        return new ExchangeRate
        {
            Id = dto.Id,
            BaseCurrency = baseCurrency,
            TargetCurrency = targetCurrency,
            Rate = dto.Rate
        };
    }

    public async Task<IEnumerable<ExchangeRate>> GetAllAsync()
    {
        var dtoList = await exchangeRateDAO.GetAllAsync();

        var result = new List<ExchangeRate>();

        foreach (var dto in dtoList)
        {
            var baseCurrency = await currencyRepository.GetByIdAsync(dto.BaseCurrencyId);
            var targetCurrency = await currencyRepository.GetByIdAsync(dto.TargetCurrencyId);

            var exchangeRate = new ExchangeRate
            {
                Id = dto.Id,
                BaseCurrency = baseCurrency,
                TargetCurrency = targetCurrency,
                Rate = dto.Rate
            };

            result.Add(exchangeRate);
        }

        return result;
    }

    public async Task<IEnumerable<ExchangeRate>> GetAllAsync(int pageSize, int pageNumber)
    {
        var dtoList = await exchangeRateDAO.GetAllAsync(pageSize, pageNumber);

        var result = new List<ExchangeRate>();

        foreach (var dto in dtoList)
        {
            var baseCurrency = await currencyRepository.GetByIdAsync(dto.BaseCurrencyId);
            var targetCurrency = await currencyRepository.GetByIdAsync(dto.TargetCurrencyId);

            var exchangeRate = new ExchangeRate
            {
                Id = dto.Id,
                BaseCurrency = baseCurrency,
                TargetCurrency = targetCurrency,
                Rate = dto.Rate
            };

            result.Add(exchangeRate);
        }

        return result;
    }

    public async Task<bool> DeleteAsync(ExchangeRate entity)
    {
        return await exchangeRateDAO.DeleteAsync(entity.Id);
    }

    public async Task<bool> UpdateAsync(ExchangeRate entity)
    {
        var dto = new UpdateExchangeRateDTO
        {
            Id = entity.Id,
            BaseCurrencyId = entity.BaseCurrency.Id,
            TargetCurrencyId = entity.TargetCurrency.Id,
            Rate = entity.Rate
        };
        return await exchangeRateDAO.UpdateAsync(dto);
    }
}