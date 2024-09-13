using CurrencyExchange.Domain.Entities;
using CurrencyExchange.Domain.Enums;
using CurrencyExchange.Domain.Response;
using CurrencyExchange.Service.DTOs;
using CurrencyExchange.Service.Interfaces;

namespace CurrencyExchange.Service.Implementations;

public class CurrencyService(IBaseRepository<Currency> currencyRepository) : ICurrencyService
{
    public async Task<BaseResponse<IEnumerable<CurrencyDTO>>> GetCurrenciesAsync(int pageSize, int pageNumber)
    {
        try
        {
            var currencies = await currencyRepository.GetAll(pageSize, pageNumber);

            var dto = currencies.Select(currency => new CurrencyDTO
            {
                Id = currency.Id,
                Code = currency.Code,
                FullName = currency.FullName,
                Sign = currency.Sign
            });

            return new BaseResponse<IEnumerable<CurrencyDTO>>
            {
                Message = new MessageText("Success"),
                Data = dto,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<CurrencyDTO>>
            {
                Message = new MessageText($"[GetCurrencies]: {ex.Message}"),
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<CurrencyDTO>> GetCurrencyByIdAsync(int id)
    {
        try
        {
            var currency = await currencyRepository.GetById(id);

            if (currency == null)
                return new BaseResponse<CurrencyDTO>
                {
                    Message = new MessageText("Currency not found"),
                    StatusCode = StatusCode.NotFound
                };

            var dto = new CurrencyDTO
            {
                Id = currency.Id,
                Code = currency.Code,
                FullName = currency.FullName,
                Sign = currency.Sign
            };

            return new BaseResponse<CurrencyDTO>
            {
                Message = new MessageText("Success"),
                StatusCode = StatusCode.OK,
                Data = dto
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<CurrencyDTO>
            {
                Message = new MessageText($"[GetCurrencyById] : {ex.Message}"),
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<bool>> CreateCurrencyAsync(CurrencyDTO dto)
    {
        try
        {
            var currency = new Currency
            {
                Code = dto.Code,
                FullName = dto.FullName,
                Sign = dto.Sign
            };

            var isCreated = await currencyRepository.Create(currency);

            if (!isCreated)
                return new BaseResponse<bool>
                {
                    Message = new MessageText("Error creating new currency"),
                    StatusCode = StatusCode.Conflict,
                    Data = false
                };

            return new BaseResponse<bool>
            {
                Message = new MessageText("Success"),
                StatusCode = StatusCode.Created,
                Data = true
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>
            {
                Message = new MessageText($"[CreateCurrency] : {ex.Message}"),
                StatusCode = StatusCode.InternalServerError,
                Data = false
            };
        }
    }

    public async Task<BaseResponse<bool>> UpdateCurrencyAsync(int id, CurrencyDTO dto)
    {
        try
        {
            var currency = await currencyRepository.GetById(id);

            if (currency == null)
                return new BaseResponse<bool>
                {
                    Message = new MessageText("The currency that should be updated was not found"),
                    StatusCode = StatusCode.NotFound,
                    Data = false
                };

            {
                currency.Code = dto.Code;
                currency.FullName = dto.FullName;
                currency.Sign = dto.Sign;
            }

            var isUpdated = await currencyRepository.Update(currency);

            if (!isUpdated)
                return new BaseResponse<bool>
                {
                    Message = new MessageText("Error updating new currency"),
                    StatusCode = StatusCode.BadRequest,
                    Data = false
                };

            return new BaseResponse<bool>
            {
                Message = new MessageText("Success"),
                StatusCode = StatusCode.OK,
                Data = true
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>
            {
                Message = new MessageText($"[UpdateCurrency] : {ex.Message}"),
                StatusCode = StatusCode.InternalServerError,
                Data = false
            };
        }
    }

    public async Task<BaseResponse<bool>> DeleteCurrencyAsync(int id)
    {
        try
        {
            var currency = await currencyRepository.GetById(id);

            if (currency == null)
                return new BaseResponse<bool>
                {
                    Message = new MessageText("Currency not found"),
                    StatusCode = StatusCode.NotFound,
                    Data = false
                };

            var isDeleted = await currencyRepository.Delete(currency);

            if (!isDeleted)
                return new BaseResponse<bool>
                {
                    Message = new MessageText("Error deleting currency"),
                    StatusCode = StatusCode.BadRequest,
                    Data = false
                };

            return new BaseResponse<bool>
            {
                Message = new MessageText("Success"),
                StatusCode = StatusCode.OK,
                Data = true
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>
            {
                Message = new MessageText($"[DeleteCurrency] : {ex.Message}"),
                StatusCode = StatusCode.InternalServerError,
                Data = false
            };
        }
    }

    public async Task<BaseResponse<IEnumerable<CurrencyDTO>>> GetCurrenciesAsync()
    {
        try
        {
            var currencies = await currencyRepository.GetAll();

            var dto = currencies.Select(currency => new CurrencyDTO
            {
                Id = currency.Id,
                Code = currency.Code,
                FullName = currency.FullName,
                Sign = currency.Sign
            });

            return new BaseResponse<IEnumerable<CurrencyDTO>>
            {
                Message = new MessageText("Success"),
                Data = dto,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<CurrencyDTO>>
            {
                Message = new MessageText($"[GetCurrencies]: {ex.Message}"),
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}