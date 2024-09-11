using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Entity;
using CurrencyExchange.Domain.Enum;
using CurrencyExchange.Domain.Response;
using CurrencyExchange.DTOs.Currency;
using CurrencyExchange.Service.Interfaces;

namespace CurrencyExchange.Service.Implementations;

public class CurrencyService(IBaseRepository<Currency> currencyRepository) : ICurrencyService
{
    public Task<BaseResponse<IEnumerable<GetCurrencyDTO>>> GetCurrenciesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<IEnumerable<GetCurrencyDTO>>> GetCurrenciesAsync(int pageSize, int pageNumber)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<GetCurrencyDTO>> GetCurrencyByIdAsync(int id)
    {
        try
        {
            var currency = await currencyRepository.GetByIdAsync(id);
            
            if (currency == null)
            {
                return new BaseResponse<GetCurrencyDTO>()
                {
                    Description = "Currency not found.",
                    StatusCode = StatusCode.NotFound
                };
            }

            var dto = new GetCurrencyDTO()
            {
                Id = currency.Id,
                Code = currency.Code,
                FullName = currency.FullName,
                Sign = currency.Sign
            };

            return new BaseResponse<GetCurrencyDTO>()
            {
                Description = "Successfully retrieved.",
                StatusCode = StatusCode.OK,
                Data = dto
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<GetCurrencyDTO>()
            {
                Description = $"[GetCurrencyDTO] : {ex.Message}",
                StatusCode = StatusCode.IternalServerError
            };
        }
        
    }

    public Task<BaseResponse<bool>> CreateCurrencyAsync(CreateCurrencyDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<bool>> UpdateCurrencyAsync(UpdateCurrencyDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<bool>> DeleteCurrencyAsync(int id)
    {
        throw new NotImplementedException();
    }
}