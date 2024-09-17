using CurrencyExchange.Contracts.Currency;
using CurrencyExchange.Contracts.Currency.DTOs;
using CurrencyExchange.DAL.Entities;
using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Enums;
using CurrencyExchange.Domain.Helpers;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Response.Implementations;
using CurrencyExchange.Domain.Response.Interfaces;
using CurrencyExchange.Service.Interfaces;

namespace CurrencyExchange.Service.Implementations;

public class CurrencyService(IRepository<Currency, CurrencyEntity> currencyRepository)
    : IService<CreateCurrencyDTO, UpdateCurrencyDTO>
{
    public async Task<IResponse> CreateAsync(CreateCurrencyDTO dto)
    {
        try
        {
            var domainResult = Currency.Create(Guid.NewGuid(), dto.Code, dto.FullName, dto.Sign);

            if (domainResult.errors.Count > 0 || domainResult.currency == null)
                return new FailedErrorResponse(
                    statusCode: (int)StatusCode.UnprocessableEntity,
                    errors: domainResult.errors);

            var dbResult = await currencyRepository.CreateAsync(domainResult.currency);

            if (!dbResult.IsSuccess || dbResult.Data == null)
                return new FailedResponse(
                    statusCode: (int)StatusCode.Conflict
                );

            return new SuccessDataResponse<CurrencyDTO>(
                status: EnumHelper.GetEnumDisplayName(OperationStatus.Created),
                message: EnumHelper.GetEnumDescription(OperationStatus.Created),
                statusCode: (int)StatusCode.Created,
                data: new CurrencyDTO(
                    domainResult.currency.Id,
                    domainResult.currency.Code,
                    domainResult.currency.FullName,
                    domainResult.currency.Sign)
            );
        }
        catch (Exception ex)
        {
            return new FailedResponse(
                statusCode: (int)StatusCode.InternalServerError,
                status: EnumHelper.GetEnumDescription(OperationStatus.Failed),
                message: $"[Create]: {ex.Message}"
            );
        }
    }

    public async Task<IResponse> GetAllAsync(int pageSize, int pageNumber)
    {
        try
        {
            var dbResult = await currencyRepository.GetAllAsync(pageSize, pageNumber);

            if (!dbResult.IsSuccess || dbResult.Data == null)
                return new FailedResponse(
                    statusCode: (int)StatusCode.NotFound
                );

            return new SuccessDataResponse<IEnumerable<CurrencyDTO>>(
                status: EnumHelper.GetEnumDisplayName(OperationStatus.Received),
                message: EnumHelper.GetEnumDescription(OperationStatus.Received),
                statusCode: (int)StatusCode.OK,
                data: dbResult.Data.Select(x => new CurrencyDTO(
                    Guid.Parse(x.Id),
                    x.Code,
                    x.FullName,
                    x.Sign)));
        }
        catch (Exception ex)
        {
            return new FailedResponse(
                statusCode: (int)StatusCode.InternalServerError,
                status: EnumHelper.GetEnumDescription(OperationStatus.Failed),
                message: $"[GetAll]: {ex.Message}"
            );
        }
    }

    public async Task<IResponse> GetByIdAsync(Guid id)
    {
        try
        {
            var dbResult = await currencyRepository.GetByIdAsync(id);

            if (!dbResult.IsSuccess || dbResult.Data == null)
                return new FailedResponse(
                    statusCode: (int)StatusCode.NotFound
                );


            return new SuccessDataResponse<CurrencyDTO>(
                status: EnumHelper.GetEnumDisplayName(OperationStatus.Received),
                message: EnumHelper.GetEnumDescription(OperationStatus.Received),
                statusCode: (int)StatusCode.OK,
                data: new CurrencyDTO(
                    Guid.Parse(dbResult.Data.Id),
                    dbResult.Data.Code,
                    dbResult.Data.FullName,
                    dbResult.Data.Sign));
        }
        catch (Exception ex)
        {
            return new FailedResponse(
                statusCode: (int)StatusCode.InternalServerError,
                status: EnumHelper.GetEnumDescription(OperationStatus.Failed),
                message: $"[GetById]: {ex.Message}"
            );
        }
    }

    public async Task<IResponse> UpdateAsync(Guid id, UpdateCurrencyDTO dto)
    {
        try
        {
            var domainResult = Currency.Create(id, dto.Code, dto.FullName, dto.Sign);

            if (domainResult.errors.Count > 0 || domainResult.currency == null)
                return new FailedErrorResponse(
                    statusCode: (int)StatusCode.UnprocessableEntity,
                    errors: domainResult.errors);

            var dbResult = await currencyRepository.UpdateAsync(id, domainResult.currency);

            // or notfound
            if (!dbResult.IsSuccess || dbResult.Data == null)
                return new FailedResponse(
                    statusCode: (int)StatusCode.Conflict
                );

            return new SuccessDataResponse<CurrencyDTO>(
                status: EnumHelper.GetEnumDisplayName(OperationStatus.Updated),
                message: EnumHelper.GetEnumDescription(OperationStatus.Updated),
                statusCode: (int)StatusCode.OK,
                data: new CurrencyDTO(
                    dbResult.Data.Id,
                    dbResult.Data.Code,
                    dbResult.Data.FullName,
                    dbResult.Data.Sign));
        }
        catch (Exception ex)
        {
            return new FailedResponse(
                statusCode: (int)StatusCode.InternalServerError,
                status: EnumHelper.GetEnumDescription(OperationStatus.Failed),
                message: $"[Update]: {ex.Message}"
            );
        }
    }

    public async Task<IResponse> DeleteAsync(Guid id)
    {
        try
        {
            var dbResult = await currencyRepository.DeleteAsync(id);

            if (!dbResult.IsSuccess)
                return new FailedResponse(
                    statusCode: (int)StatusCode.NotFound
                );

            return new SuccessDataResponse<Guid>(
                status: EnumHelper.GetEnumDisplayName(OperationStatus.Deleted),
                message: EnumHelper.GetEnumDescription(OperationStatus.Deleted),
                statusCode: (int)StatusCode.OK,
                data: id);
        }
        catch (Exception ex)
        {
            return new FailedResponse(
                statusCode: (int)StatusCode.InternalServerError,
                status: EnumHelper.GetEnumDescription(OperationStatus.Failed),
                message: $"[Delete]: {ex.Message}"
            );
        }
    }
}