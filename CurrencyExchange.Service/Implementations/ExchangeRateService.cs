using CurrencyExchange.DAL.DAO.Interfaces;
using CurrencyExchange.Domain.Entities;
using CurrencyExchange.Domain.Enums;
using CurrencyExchange.Domain.Response;
using CurrencyExchange.Service.DTOs;
using CurrencyExchange.Service.Interfaces;

namespace CurrencyExchange.Service.Implementations;

public class ExchangeRateService(IBaseRepository<ExchangeRate> exchangeRateRepository) : IExchangeRateService
{
    public async Task<BaseResponse<IEnumerable<ExchangeRateDTO>>> GetCurrenciesAsync()
    {
        try
        {
            var exchangeRates = await exchangeRateRepository.GetAll();

            var dto = exchangeRates.Select(exchangeRate => new ExchangeRateDTO
            {
                Id = exchangeRate.Id,
                BaseCurrency = new CurrencyDTO
                {
                    Id = exchangeRate.BaseCurrency.Id,
                    FullName = exchangeRate.BaseCurrency.FullName,
                    Code = exchangeRate.BaseCurrency.Code,
                    Sign = exchangeRate.BaseCurrency.Sign
                },
                TargetCurrency = new CurrencyDTO
                {
                    Id = exchangeRate.TargetCurrency.Id,
                    FullName = exchangeRate.TargetCurrency.FullName,
                    Code = exchangeRate.TargetCurrency.Code,
                    Sign = exchangeRate.TargetCurrency.Sign
                },
                Rate = exchangeRate.Rate
            });

            return new BaseResponse<IEnumerable<ExchangeRateDTO>>
            {
                Message = new MessageText("Success"),
                Data = dto,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<ExchangeRateDTO>>
            {
                Message = new MessageText($"[GetExchangeRates]: {ex.Message}"),
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<IEnumerable<ExchangeRateDTO>>> GetCurrenciesAsync(int pageSize, int pageNumber)
    {
        try
        {
            var exchangeRates = await exchangeRateRepository.GetAll(pageSize, pageNumber);

            var dto = exchangeRates.Select(exchangeRate => new ExchangeRateDTO
            {
                Id = exchangeRate.Id,
                BaseCurrency = new CurrencyDTO
                {
                    Id = exchangeRate.BaseCurrency.Id,
                    FullName = exchangeRate.BaseCurrency.FullName,
                    Code = exchangeRate.BaseCurrency.Code,
                    Sign = exchangeRate.BaseCurrency.Sign
                },
                TargetCurrency = new CurrencyDTO
                {
                    Id = exchangeRate.TargetCurrency.Id,
                    FullName = exchangeRate.TargetCurrency.FullName,
                    Code = exchangeRate.TargetCurrency.Code,
                    Sign = exchangeRate.TargetCurrency.Sign
                },
                Rate = exchangeRate.Rate
            });

            return new BaseResponse<IEnumerable<ExchangeRateDTO>>
            {
                Message = new MessageText("Success"),
                Data = dto,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<ExchangeRateDTO>>
            {
                Message = new MessageText($"[GetExchangeRates]: {ex.Message}"),
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<ExchangeRateDTO>> GetCurrencyByIdAsync(int id)
    {
        try
        {
            var exchangeRate = await exchangeRateRepository.GetById(id);

            if (exchangeRate == null)
                return new BaseResponse<ExchangeRateDTO>
                {
                    Message = new MessageText("ExchangeRate not found"),
                    StatusCode = StatusCode.NotFound
                };

            var dto = new ExchangeRateDTO
            {
                Id = exchangeRate.Id,
                BaseCurrency = new CurrencyDTO
                {
                    Id = exchangeRate.BaseCurrency.Id,
                    FullName = exchangeRate.BaseCurrency.FullName,
                    Code = exchangeRate.BaseCurrency.Code,
                    Sign = exchangeRate.BaseCurrency.Sign
                },
                TargetCurrency = new CurrencyDTO
                {
                    Id = exchangeRate.TargetCurrency.Id,
                    FullName = exchangeRate.TargetCurrency.FullName,
                    Code = exchangeRate.TargetCurrency.Code,
                    Sign = exchangeRate.TargetCurrency.Sign
                },
                Rate = exchangeRate.Rate
            };

            return new BaseResponse<ExchangeRateDTO>
            {
                Message = new MessageText("Success"),
                StatusCode = StatusCode.OK,
                Data = dto
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ExchangeRateDTO>
            {
                Message = new MessageText($"[GetExchangeRateBytId] : {ex.Message}"),
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<bool>> CreateCurrencyAsync(ExchangeRateDTO dto)
    {
        try
        {
            var exchangeRate = new ExchangeRate
            {
                BaseCurrency = new Currency
                {
                    Id = dto.BaseCurrency.Id,
                    Code = dto.BaseCurrency.Code,
                    FullName = dto.BaseCurrency.FullName,
                    Sign = dto.BaseCurrency.Sign
                },
                TargetCurrency = new Currency
                {
                    Id = dto.TargetCurrency.Id,
                    Code = dto.TargetCurrency.Code,
                    FullName = dto.TargetCurrency.FullName,
                    Sign = dto.TargetCurrency.Sign
                },
                Rate = dto.Rate
            };

            var isCreated = await exchangeRateRepository.Create(exchangeRate);

            if (!isCreated)
                return new BaseResponse<bool>
                {
                    Message = new MessageText("Error creating new exchangeRate"),
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
                Message = new MessageText($"[CreateExchangeRate] : {ex.Message}"),
                StatusCode = StatusCode.InternalServerError,
                Data = false
            };
        }
    }

    public async Task<BaseResponse<bool>> UpdateCurrencyAsync(int id, ExchangeRateDTO dto)
    {
        try
        {
            var exchangeRate = await exchangeRateRepository.GetById(id);

            if (exchangeRate == null)
                return new BaseResponse<bool>
                {
                    Message = new MessageText("The exchange rate that should be updated was not found"),
                    StatusCode = StatusCode.NotFound,
                    Data = false
                };

            {
                exchangeRate.BaseCurrency = new Currency
                {
                    Id = dto.BaseCurrency.Id,
                    Code = dto.BaseCurrency.Code,
                    FullName = dto.BaseCurrency.FullName,
                    Sign = dto.BaseCurrency.Sign
                };
                exchangeRate.TargetCurrency = new Currency
                {
                    Id = dto.TargetCurrency.Id,
                    Code = dto.TargetCurrency.Code,
                    FullName = dto.TargetCurrency.FullName,
                    Sign = dto.TargetCurrency.Sign
                };
                exchangeRate.Rate = dto.Rate;
            }

            var isUpdated = await exchangeRateRepository.Update(exchangeRate);

            if (!isUpdated)
                return new BaseResponse<bool>
                {
                    Message = new MessageText("Error updating new exchangeRate"),
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
                Message = new MessageText($"[UpdateExchangeRate] : {ex.Message}"),
                StatusCode = StatusCode.InternalServerError,
                Data = false
            };
        }
    }

    public async Task<BaseResponse<bool>> DeleteCurrencyAsync(int id)
    {
        try
        {
            var exchangeRate = await exchangeRateRepository.GetById(id);

            if (exchangeRate == null)
                return new BaseResponse<bool>
                {
                    Message = new MessageText("Exchange rate not found"),
                    StatusCode = StatusCode.NotFound,
                    Data = false
                };

            var isDeleted = await exchangeRateRepository.Delete(exchangeRate);

            if (!isDeleted)
                return new BaseResponse<bool>
                {
                    Message = new MessageText("Error deleting exchange rate"),
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
                Message = new MessageText($"[DeleteExchangeRate] : {ex.Message}"),
                StatusCode = StatusCode.InternalServerError,
                Data = false
            };
        }
    }
}