using CurrencyExchange.DAL.DAO.DTOs.ExchangeRate;
using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Entity;
using CurrencyExchange.Domain.Response;
using CurrencyExchange.Service.Interfaces;

namespace CurrencyExchange.Service.Implementations;

public class ExchangeRateService(IBaseRepository<ExchangeRate> exchangeRateRepository) : IExchangeRateService
{
    public async Task<BaseResponse<IEnumerable<GetExchangeRateDTO>>> GetExchangeRatesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<IEnumerable<GetExchangeRateDTO>>> GetExchangeRatesAsync(int pageSize, int pageNumber)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetExchangeRateDTO>> GetExchangeRateByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<bool>> CreateExchangeRateAsync(CreateExchangeRateDTO dto)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<bool>> UpdateExchangeRateAsync(UpdateExchangeRateDTO dto)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<bool>> DeleteExchangeRateAsync(int id)
    {
        throw new NotImplementedException();
    }
}