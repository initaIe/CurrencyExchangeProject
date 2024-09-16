using CurrencyExchange.Contracts.Currency;
using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Enums;
using CurrencyExchange.Domain.Helpers;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Response.Implementations;
using CurrencyExchange.Domain.Response.Interfaces;
using CurrencyExchange.Service.Interfaces;

namespace CurrencyExchange.Service.Implementations;

public class CurrencyService(ICurrencyRepository currencyRepository)
    : ICurrencyService
{
    public async Task<IResponse> CreateAsync(CreateCurrencyDTO dto)
    {
        try
        {
            var domainResult = Currency.Create(Guid.NewGuid(), dto.Code, dto.FullName, dto.Sign);

            if (domainResult.errors.Count > 0 || domainResult.currency == null)
                return new FailedErrorResponse(
                    (int)StatusCode.UnprocessableEntity,
                    domainResult.errors);

            var dbResult = await currencyRepository.Create(domainResult.currency);

            if (!dbResult.IsSuccess || dbResult.Data == null)
                return new FailedResponse(
                    (int)StatusCode.Conflict
                );

            return new SuccessDataResponse<CurrencyDTO>(
                EnumHelper.GetEnumDisplayName(OperationStatus.Created),
                EnumHelper.GetEnumDescription(OperationStatus.Created),
                (int)StatusCode.Created,
                new CurrencyDTO(
                    domainResult.currency.Id,
                    domainResult.currency.Code,
                    domainResult.currency.FullName,
                    domainResult.currency.Sign)
            );
        }
        catch (Exception ex)
        {
            return new FailedResponse(
                (int)StatusCode.InternalServerError,
                EnumHelper.GetEnumDescription(OperationStatus.Failed),
                $"[CreateCurrency]: {ex.Message}"
            );
        }
    }

    public async Task<IResponse> GetAllAsync(int pageSize, int pageNumber)
    {
        try
        {
            var dbResult = await currencyRepository.GetAll(pageSize, pageNumber);

            if (!dbResult.IsSuccess || dbResult.Data == null)
                return new FailedResponse(
                    (int)StatusCode.NotFound
                );

            return new SuccessDataResponse<IEnumerable<CurrencyDTO>>(
                EnumHelper.GetEnumDisplayName(OperationStatus.Received),
                EnumHelper.GetEnumDescription(OperationStatus.Received),
                (int)StatusCode.OK,
                dbResult.Data.Select(x => new CurrencyDTO(
                    Guid.Parse(x.Id),
                    x.Code,
                    x.FullName,
                    x.Sign)));
        }
        catch (Exception ex)
        {
            return new FailedResponse(
                (int)StatusCode.InternalServerError,
                EnumHelper.GetEnumDescription(OperationStatus.Failed),
                $"[GetCurrencies]: {ex.Message}"
            );
        }
    }

    public async Task<IResponse> GetByIdAsync(Guid id)
    {
        try
        {
            var dbResult = await currencyRepository.GetById(id);

            if (!dbResult.IsSuccess || dbResult.Data == null)
                return new FailedResponse(
                    (int)StatusCode.NotFound
                );


            return new SuccessDataResponse<CurrencyDTO>(
                EnumHelper.GetEnumDisplayName(OperationStatus.Received),
                EnumHelper.GetEnumDescription(OperationStatus.Received),
                (int)StatusCode.OK,
                new CurrencyDTO(
                    Guid.Parse(dbResult.Data.Id),
                    dbResult.Data.Code,
                    dbResult.Data.FullName,
                    dbResult.Data.Sign));
        }
        catch (Exception ex)
        {
            return new FailedResponse(
                (int)StatusCode.InternalServerError,
                EnumHelper.GetEnumDescription(OperationStatus.Failed),
                $"[GetCurrencyById]: {ex.Message}"
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
                    (int)StatusCode.UnprocessableEntity,
                    domainResult.errors);

            var dbResult = await currencyRepository.Update(id, domainResult.currency);

            // or notfound
            if (!dbResult.IsSuccess || dbResult.Data == null)
                return new FailedResponse(
                    (int)StatusCode.Conflict
                );

            return new SuccessDataResponse<CurrencyDTO>(
                EnumHelper.GetEnumDisplayName(OperationStatus.Updated),
                EnumHelper.GetEnumDescription(OperationStatus.Updated),
                (int)StatusCode.OK,
                new CurrencyDTO(
                    dbResult.Data.Id,
                    dbResult.Data.Code,
                    dbResult.Data.FullName,
                    dbResult.Data.Sign));
        }
        catch (Exception ex)
        {
            return new FailedResponse(
                (int)StatusCode.InternalServerError,
                EnumHelper.GetEnumDescription(OperationStatus.Failed),
                $"[UpdateCurrency]: {ex.Message}"
            );
        }
    }

    public async Task<IResponse> DeleteAsync(Guid id)
    {
        try
        {
            var dbResult = await currencyRepository.Delete(id);

            if (!dbResult.IsSuccess)
                return new FailedResponse(
                    (int)StatusCode.NotFound
                );

            return new SuccessDataResponse<Guid>(
                EnumHelper.GetEnumDisplayName(OperationStatus.Deleted),
                EnumHelper.GetEnumDescription(OperationStatus.Deleted),
                (int)StatusCode.OK,
                id);
        }
        catch (Exception ex)
        {
            return new FailedResponse(
                (int)StatusCode.InternalServerError,
                EnumHelper.GetEnumDescription(OperationStatus.Failed),
                $"[DeleteCurrency]: {ex.Message}"
            );
        }
    }
}