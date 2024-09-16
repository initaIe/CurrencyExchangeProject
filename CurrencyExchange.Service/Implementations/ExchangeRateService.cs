using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Enums;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Response;
using CurrencyExchange.Service.DTOs;
using CurrencyExchange.Service.Interfaces;

namespace CurrencyExchange.Service.Implementations;

public class ExchangeRateService(IExchangeRateRepository exchangeRateRepository) : IExchangeRateService
{
    public async Task<BaseResponse<IEnumerable<ExchangeRateDTO>>> GetCurrenciesAsync(int pageSize, int pageNumber)
    {
        try
        {
            var result = await exchangeRateRepository.GetAll(pageSize, pageNumber);

            if (!result.IsSuccess || result.Data == null)
                return new BaseResponse<IEnumerable<ExchangeRateDTO>>
                {
                    Message = new MessageText(result.Status!, "ExchangeRate getAll operation failed"),
                    StatusCode = StatusCode.NotFound
                };

            return new BaseResponse<IEnumerable<ExchangeRateDTO>>
            {
                Message = new MessageText(result.Status!, "Success"),
                StatusCode = StatusCode.OK,
                Data = result.Data!.Select(x => new ExchangeRateDTO
                {
                    Id = Guid.Parse(x.Id),
                    BaseCurrency = new CurrencyDTO
                    {
                        Id = Guid.Parse(x.BaseCurrency.Id),
                        Code = x.BaseCurrency.Code,
                        FullName = x.BaseCurrency.FullName,
                        Sign = x.BaseCurrency.Sign
                    },
                    TargetCurrency = new CurrencyDTO
                    {
                        Id = Guid.Parse(x.TargetCurrency.Id),
                        Code = x.TargetCurrency.Code,
                        FullName = x.TargetCurrency.FullName,
                        Sign = x.TargetCurrency.Sign
                    },
                    Rate = x.Rate
                })
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

    public async Task<BaseResponse<ExchangeRateDTO>> GetCurrencyByIdAsync(Guid id)
    {
        try
        {
            var result = await exchangeRateRepository.GetById(id);

            if (!result.IsSuccess || result.Data == null)
                return new BaseResponse<ExchangeRateDTO>
                {
                    Message = new MessageText(result.Status!, "ExchangeRate get operation failed"),
                    StatusCode = StatusCode.NotFound
                };


            return new BaseResponse<ExchangeRateDTO>
            {
                Message = new MessageText(result.Status!, "Success"),
                StatusCode = StatusCode.OK,
                Data = new ExchangeRateDTO
                {
                    Id = Guid.Parse(result.Data.Id),
                    BaseCurrency = new CurrencyDTO
                    {
                        Id = Guid.Parse(result.Data.BaseCurrency.Id),
                        Code = result.Data.BaseCurrency.Code,
                        FullName = result.Data.BaseCurrency.FullName,
                        Sign = result.Data.BaseCurrency.Sign
                    },
                    TargetCurrency = new CurrencyDTO
                    {
                        Id = Guid.Parse(result.Data.TargetCurrency.Id),
                        Code = result.Data.TargetCurrency.Code,
                        FullName = result.Data.TargetCurrency.FullName,
                        Sign = result.Data.TargetCurrency.Sign
                    },
                    Rate = result.Data.Rate
                }
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

    public async Task<BaseResponse<ExchangeRate>> CreateCurrencyAsync(ExchangeRate exchangeRate)
    {
        try
        {
            var result = await exchangeRateRepository.Create(exchangeRate);

            if (!result.IsSuccess)
                return new BaseResponse<ExchangeRate>
                {
                    Message = new MessageText(result.Status!, "ExchangeRate create operation failed"),
                    StatusCode = StatusCode.Conflict
                };

            return new BaseResponse<ExchangeRate>
            {
                Message = new MessageText(result.Status!, "Success"),
                StatusCode = StatusCode.Created,
                Data = exchangeRate
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ExchangeRate>
            {
                Message = new MessageText($"[CreateExchangeRate] : {ex.Message}"),
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<ExchangeRate>> UpdateCurrencyAsync(Guid id, ExchangeRate exchangeRate)
    {
        try
        {
            var result = await exchangeRateRepository.Update(id, exchangeRate);

            if (!result.IsSuccess)
                return new BaseResponse<ExchangeRate>
                {
                    Message = new MessageText(result.Status!, "ExchangeRate update operation failed"),
                    StatusCode = StatusCode.NotFound
                };

            return new BaseResponse<ExchangeRate>
            {
                Message = new MessageText(result.Status!, "Success"),
                StatusCode = StatusCode.OK,
                Data = exchangeRate
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ExchangeRate>
            {
                Message = new MessageText($"[UpdateExchangeRate] : {ex.Message}"),
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<Guid>> DeleteCurrencyAsync(Guid id)
    {
        try
        {
            var result = await exchangeRateRepository.Delete(id);

            if (!result.IsSuccess)
                return new BaseResponse<Guid>
                {
                    Message = new MessageText(result.Status!,"ExchangeRate delete operation failed"),
                    StatusCode = StatusCode.NotFound
                };

            return new BaseResponse<Guid>
            {
                Message = new MessageText("Success"),
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Guid>
            {
                Message = new MessageText($"[DeleteExchangeRate] : {ex.Message}"),
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}