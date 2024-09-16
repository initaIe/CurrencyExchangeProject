using CurrencyExchange.Contracts.Currency;
using CurrencyExchange.DAL.Entities;
using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Enums;
using CurrencyExchange.Domain.Helpers;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Response;
using CurrencyExchange.Service.DTOs;
using CurrencyExchange.Service.Interfaces;

namespace CurrencyExchange.Service.Implementations;

public class CurrencyService(ICurrencyRepository currencyRepository)
    : ICurrencyService
{
    public async Task<IBaseResponse<CurrencyDTO>> CreateCurrencyAsync(CreateCurrencyDTO dto)
    {
        try
        {
            var modelCreationResult = Currency.Create
                (Guid.NewGuid(), request.Code, request.FullName, request.Sign);


            if (!string.IsNullOrEmpty(modelCreationResult.error) || modelCreationResult.currency == null)
                return new BaseResponse<CurrencyResponse>
                {
                    Message = new MessageText(modelCreationResult.error),
                    StatusCode = StatusCode.BadRequest
                };

            var dbCreationResult = await currencyRepository.Create(modelCreationResult.currency);

            if (!dbCreationResult.IsSuccess || dbCreationResult.Data == null)
                return new BaseResponse<CurrencyResponse>
                {
                    Message = new MessageText(EnumHelper.GetEnumDescription(dbCreationResult.Status)),
                    StatusCode = StatusCode.Conflict
                };

            return new BaseResponse<CurrencyResponse>
            {
                Message = new MessageText(EnumHelper.GetEnumDescription(StatusCode.Created)),
                StatusCode = StatusCode.Created,
                Data = new CurrencyResponse(
                    dbCreationResult.Data.Id,
                    dbCreationResult.Data.Code,
                    dbCreationResult.Data.FullName,
                    dbCreationResult.Data.Sign)
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<CurrencyResponse>
            {
                Message = new MessageText($"[Create currency]: {ex.Message}"),
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<IEnumerable<CurrencyResponse>>> GetCurrenciesAsync(int pageSize, int pageNumber)
    {
        try
        {
            var result = await currencyRepository.GetAll(pageSize, pageNumber);

            if (!result.IsSuccess)
                return new BaseResponse<IEnumerable<CurrencyResponse>>
                {
                    Message = new MessageText(EnumHelper.GetEnumDescription(result.Status)),
                    StatusCode = StatusCode.NotFound
                };

            return new BaseResponse<IEnumerable<CurrencyResponse>>
            {
                Message = new MessageText("Success"),
                StatusCode = StatusCode.OK,
                Data = result.Data!.Select(x => new CurrencyDTO
                {
                    Id = Guid.Parse(x.Id),
                    Code = x.Code,
                    FullName = x.FullName,
                    Sign = x.Sign
                })
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<CurrencyDTO>>
            {
                Message = new MessageText($"[Get currencies]: {ex.Message}"),
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<CurrencyResponse>> GetCurrencyByIdAsync(Guid id)
    {
        try
        {
            var result = await currencyRepository.GetById(id);

            if (!result.IsSuccess || result.Data == null)
                return new BaseResponse<CurrencyDTO>
                {
                    Message = new MessageText(result.Status!, "Currency get operation failed"),
                    StatusCode = StatusCode.NotFound
                };

            var dto = new CurrencyDTO
            {
                Id = Guid.Parse(result.Data.Id),
                Code = result.Data.Code,
                FullName = result.Data.FullName,
                Sign = result.Data.Sign
            };

            return new BaseResponse<CurrencyDTO>
            {
                Message = new MessageText(result.Status!, "Success"),
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

    public async Task<BaseResponse<CurrencyResponse>> UpdateCurrencyAsync(Guid id, UpdateCurrencyRequest request)
    {
        try
        {
            var result = await currencyRepository.Update(id, currency);

            if (!result.IsSuccess)
                return new BaseResponse<Currency>
                {
                    Message = new MessageText(result.Status!,
                        "Currency update operation failed"),
                    StatusCode = StatusCode.NotFound
                };

            return new BaseResponse<Currency>
            {
                Message = new MessageText(result.Status!, "Success"),
                StatusCode = StatusCode.OK,
                Data = result.Data
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Currency>
            {
                Message = new MessageText($"[UpdateCurrency] : {ex.Message}"),
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<Guid>> DeleteCurrencyAsync(Guid id)
    {
        try
        {
            var result = await currencyRepository.Delete(id);

            if (!result.IsSuccess)
                return new BaseResponse<Guid>
                {
                    Message = new MessageText(result.Status!, "Currency delete operation failed"),
                    StatusCode = StatusCode.NotFound
                };

            return new BaseResponse<Guid>
            {
                Message = new MessageText(result.Status!, "Success"),
                StatusCode = StatusCode.OK,
                Data = id
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Guid>
            {
                Message = new MessageText($"[DeleteCurrency] : {ex.Message}"),
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}