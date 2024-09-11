using CurrencyExchange.DAL.DAO.DTOs.Currency;
using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Entity;
using CurrencyExchange.Domain.Enums;
using CurrencyExchange.Domain.Response;
using CurrencyExchange.Service.DTOs;
using CurrencyExchange.Service.Interfaces;

namespace CurrencyExchange.Service.Implementations;

public class CurrencyService(IBaseRepository<Currency> currencyRepository) : ICurrencyService
{
    public async Task<BaseResponse<IEnumerable<CurrencyDTO>>> GetCurrenciesAsync()
    {
        try
        {
            var currencies = await currencyRepository.GetAllAsync();

            var dto = currencies.Select(currency => new CurrencyDTO
            {
                Id = currency.Id,
                Code = currency.Code,
                FullName = currency.FullName,
                Sign = currency.Sign
            });

            return new BaseResponse<IEnumerable<CurrencyDTO>>
            {
                Description = "Success.",
                Data = dto,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<CurrencyDTO>>
            {
                Description = $"Internal Server Error: {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<IEnumerable<CurrencyDTO>>> GetCurrenciesAsync(int pageSize, int pageNumber)
    {
        try
        {
            var currencies = await currencyRepository.GetAllAsync(pageSize, pageNumber);

            var dto = currencies.Select(currency => new CurrencyDTO
            {
                Id = currency.Id,
                Code = currency.Code,
                FullName = currency.FullName,
                Sign = currency.Sign
            });

            return new BaseResponse<IEnumerable<CurrencyDTO>>
            {
                Description = "Success.",
                Data = dto,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<CurrencyDTO>>
            {
                Description = $"Internal Server Error: {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<CurrencyDTO>> GetCurrencyByIdAsync(int id)
    {
        try
        {
            var currency = await currencyRepository.GetByIdAsync(id);

            if (currency == null)
                return new BaseResponse<CurrencyDTO>
                {
                    Description = "Currency not found.",
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
                Description = "Success.",
                StatusCode = StatusCode.OK,
                Data = dto
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<CurrencyDTO>
            {
                Description = $"[GetCurrencyDTO] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<bool>> CreateCurrencyAsync(CurrencyDTO dto)
    {
        try
        {
            var currency = new Currency()
            {
                Code = dto.Code,
                FullName = dto.FullName,
                Sign = dto.Sign
            };

            var isCreated = await currencyRepository.CreateAsync(currency);

            if (!isCreated)
            {
                return new BaseResponse<bool>()
                {
                    Description = "Error creating new currency.",
                    StatusCode = StatusCode.Conflict,
                    Data = false
                };
            }

            return new BaseResponse<bool>()
            {
                Description = "Success.",
                StatusCode = StatusCode.OK,
                Data = true
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[Create] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError,
                Data = false
            };
        }
    }

    public async Task<BaseResponse<bool>> UpdateCurrencyAsync(CurrencyDTO dto)
    {
        try
        {
            var currency = await currencyRepository.GetByIdAsync(dto.Id);

            if (currency == null)
            {
                return new BaseResponse<bool>
                {
                    Description = "The currency that should be updated was not found.",
                    StatusCode = StatusCode.NotFound,
                    Data = false
                };
            }

            {
                currency.Code = dto.Code;
                currency.FullName = dto.FullName;
                currency.Sign = dto.Sign;
            }

            var isUpdated = await currencyRepository.UpdateAsync(currency);

            if (!isUpdated)
            {
                return new BaseResponse<bool>()
                {
                    Description = "Error updating new currency.",
                    StatusCode = StatusCode.BadRequest,
                    Data = false
                };
            }

            return new BaseResponse<bool>()
            {
                Description = "Success.",
                StatusCode = StatusCode.OK,
                Data = true
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[Update] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError,
                Data = false
            };
        }
    }

    public async Task<BaseResponse<bool>> DeleteCurrencyAsync(int id)
    {
        try
        {
            var currency = await currencyRepository.GetByIdAsync(id);
            
            if (currency == null)
            {
                return new BaseResponse<bool>()
                {
                    Description = "Currency not found.",
                    StatusCode = StatusCode.NotFound,
                    Data = false
                };
            }
            
            var isDeleted = await currencyRepository.DeleteAsync(currency);
            
            if (!isDeleted)
            {
                return new BaseResponse<bool>()
                {
                    Description = "Error deleting currency.",
                    StatusCode = StatusCode.BadRequest,
                    Data = false
                };
            }

            return new BaseResponse<bool>()
            {
                Description = "Success.",
                StatusCode = StatusCode.OK,
                Data = true
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[Delete] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError,
                Data = false
            };
        }
    }
}