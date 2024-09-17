using CurrencyExchange.Contracts.Currency;
using CurrencyExchange.Contracts.Currency.DTOs;
using CurrencyExchange.Contracts.ExchangeRate;
using CurrencyExchange.Contracts.ExchangeRate.DTOs;
using CurrencyExchange.DAL.Entities;
using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Enums;
using CurrencyExchange.Domain.Helpers;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Response.Implementations;
using CurrencyExchange.Domain.Response.Interfaces;
using CurrencyExchange.Service.Interfaces;

namespace CurrencyExchange.Service.Implementations;

public class ExchangeRateService(
    IRepository<ExchangeRate, ExchangeRateEntity> exchangeRateRepository,
    IRepository<Currency, CurrencyEntity> currencyRepository) 
    : IService<CreateExchangeRateDTO, UpdateExchangeRateDTO>
{
    public async Task<IResponse> CreateAsync(CreateExchangeRateDTO dto)
    {
        try
        {
            var baseCurrencyDbResult = await currencyRepository.GetByIdAsync(dto.BaseCurrencyId);
            var targetCurrencyDbResult = await currencyRepository.GetByIdAsync(dto.TargetCurrencyId);
            
            var baseCurrencyDomainResult = Currency.Create(
                id: Guid.Parse(baseCurrencyDbResult.Data.Id),
                code: baseCurrencyDbResult.Data.Code,
                fullName: baseCurrencyDbResult.Data.FullName,
                sign: baseCurrencyDbResult.Data.Sign
            );
            
            if (baseCurrencyDomainResult.errors.Count > 0 || baseCurrencyDomainResult.currency == null)
                return new FailedErrorResponse(
                    statusCode: (int)StatusCode.UnprocessableEntity,
                    errors: baseCurrencyDomainResult.errors);
            
            var targetCurrencyDomainResult = Currency.Create(
                id: Guid.Parse(targetCurrencyDbResult.Data.Id),
                code: targetCurrencyDbResult.Data.Code,
                fullName: targetCurrencyDbResult.Data.FullName,
                sign: targetCurrencyDbResult.Data.Sign
            );
            
            if (targetCurrencyDomainResult.errors.Count > 0 || targetCurrencyDomainResult.currency == null)
                return new FailedErrorResponse(
                    statusCode: (int)StatusCode.UnprocessableEntity,
                    errors: targetCurrencyDomainResult.errors);
            
            var exchangeRateDomainResult = ExchangeRate.Create(
                id: Guid.NewGuid(),
                baseCurrency: baseCurrencyDomainResult.currency,
                targetCurrency: targetCurrencyDomainResult.currency,
                rate: dto.Rate);
            
            if (exchangeRateDomainResult.errors.Count > 0 || exchangeRateDomainResult.exchangeRate == null)
                return new FailedErrorResponse(
                    statusCode: (int)StatusCode.UnprocessableEntity,
                    errors: exchangeRateDomainResult.errors);

            var dbResult = await exchangeRateRepository.CreateAsync(exchangeRateDomainResult.exchangeRate);
    
            if (!dbResult.IsSuccess || dbResult.Data == null)
                return new FailedResponse(
                    statusCode: (int)StatusCode.Conflict
                );
    
            return new SuccessDataResponse<ExchangeRateDTO>(
                status: EnumHelper.GetEnumDisplayName(OperationStatus.Created),
                message: EnumHelper.GetEnumDescription(OperationStatus.Created),
                statusCode: (int)StatusCode.Created,
                data: new ExchangeRateDTO(
                    Id: exchangeRateDomainResult.exchangeRate.Id,
                    BaseCurrency: new CurrencyDTO(
                        exchangeRateDomainResult.exchangeRate.BaseCurrency.Id,
                        exchangeRateDomainResult.exchangeRate.BaseCurrency.Code,
                        exchangeRateDomainResult.exchangeRate.BaseCurrency.FullName,
                        exchangeRateDomainResult.exchangeRate.BaseCurrency.Sign),
                    TargetCurrency: new CurrencyDTO(
                        exchangeRateDomainResult.exchangeRate.TargetCurrency.Id,
                        exchangeRateDomainResult.exchangeRate.TargetCurrency.Code,
                        exchangeRateDomainResult.exchangeRate.TargetCurrency.FullName,
                        exchangeRateDomainResult.exchangeRate.TargetCurrency.Sign),
                    Rate: exchangeRateDomainResult.exchangeRate.Rate)
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
            var dbResult = await exchangeRateRepository.GetAllAsync(pageSize, pageNumber);
    
            if (!dbResult.IsSuccess || dbResult.Data == null)
                return new FailedResponse(
                    statusCode: (int)StatusCode.NotFound
                );

            return new SuccessDataResponse<IEnumerable<ExchangeRateDTO>>(
                status: EnumHelper.GetEnumDisplayName(OperationStatus.Received),
                message: EnumHelper.GetEnumDescription(OperationStatus.Received),
                statusCode: (int)StatusCode.OK,
                data: dbResult.Data.Select(x => new ExchangeRateDTO(
                    Id: Guid.Parse(x.Id),
                    BaseCurrency: new CurrencyDTO(
                        Guid.Parse(x.BaseCurrency.Id),
                        x.BaseCurrency.Code,
                        x.BaseCurrency.FullName,
                        x.BaseCurrency.Sign),
                    TargetCurrency: new CurrencyDTO(
                        Guid.Parse(x.TargetCurrency.Id),
                        x.TargetCurrency.Code,
                        x.TargetCurrency.FullName,
                        x.TargetCurrency.Sign),
                    Rate: x.Rate)
                ));
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
            var dbResult = await exchangeRateRepository.GetByIdAsync(id);
    
            if (!dbResult.IsSuccess || dbResult.Data == null)
                return new FailedResponse(
                    statusCode: (int)StatusCode.NotFound
                );
    
    
            return new SuccessDataResponse<ExchangeRateDTO>(
                status: EnumHelper.GetEnumDisplayName(OperationStatus.Received),
                message: EnumHelper.GetEnumDescription(OperationStatus.Received),
                statusCode: (int)StatusCode.OK,
                data: new ExchangeRateDTO(
                    Id: Guid.Parse(dbResult.Data.Id),
                    BaseCurrency: new CurrencyDTO(
                        Guid.Parse(dbResult.Data.BaseCurrency.Id),
                        dbResult.Data.BaseCurrency.Code,
                        dbResult.Data.BaseCurrency.FullName,
                        dbResult.Data.BaseCurrency.Sign),
                    TargetCurrency: new CurrencyDTO(
                        Guid.Parse(dbResult.Data.TargetCurrency.Id),
                        dbResult.Data.TargetCurrency.Code,
                        dbResult.Data.TargetCurrency.FullName,
                        dbResult.Data.TargetCurrency.Sign),
                    Rate: dbResult.Data.Rate));
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
    public async Task<IResponse> UpdateAsync(Guid id, UpdateExchangeRateDTO dto)
    {
        try
        {
            var baseCurrencyDbResult = await currencyRepository.GetByIdAsync(dto.BaseCurrencyId);
            var targetCurrencyDbResult = await currencyRepository.GetByIdAsync(dto.TargetCurrencyId);
            
            var baseCurrencyDomainResult = Currency.Create(
                id: Guid.Parse(baseCurrencyDbResult.Data.Id),
                code: baseCurrencyDbResult.Data.Code,
                fullName: baseCurrencyDbResult.Data.FullName,
                sign: baseCurrencyDbResult.Data.Sign
            );
            
            if (baseCurrencyDomainResult.errors.Count > 0 || baseCurrencyDomainResult.currency == null)
                return new FailedErrorResponse(
                    statusCode: (int)StatusCode.UnprocessableEntity,
                    errors: baseCurrencyDomainResult.errors);
            
            var targetCurrencyDomainResult = Currency.Create(
                id: Guid.Parse(targetCurrencyDbResult.Data.Id),
                code: targetCurrencyDbResult.Data.Code,
                fullName: targetCurrencyDbResult.Data.FullName,
                sign: targetCurrencyDbResult.Data.Sign
            );
            
            if (targetCurrencyDomainResult.errors.Count > 0 || targetCurrencyDomainResult.currency == null)
                return new FailedErrorResponse(
                    statusCode: (int)StatusCode.UnprocessableEntity,
                    errors: targetCurrencyDomainResult.errors);
            
            var exchangeRateDomainResult = ExchangeRate.Create(
                id: Guid.NewGuid(),
                baseCurrency: baseCurrencyDomainResult.currency,
                targetCurrency: targetCurrencyDomainResult.currency,
                rate: dto.Rate);
            
            if (exchangeRateDomainResult.errors.Count > 0 || exchangeRateDomainResult.exchangeRate == null)
                return new FailedErrorResponse(
                    statusCode: (int)StatusCode.UnprocessableEntity,
                    errors: exchangeRateDomainResult.errors);
            
            var dbResult = await exchangeRateRepository.UpdateAsync(id, exchangeRateDomainResult.exchangeRate);
    
            if (!dbResult.IsSuccess || dbResult.Data == null)
                return new FailedResponse(
                    statusCode: (int)StatusCode.Conflict
                );
    
            return new SuccessDataResponse<ExchangeRateDTO>(
                status: EnumHelper.GetEnumDisplayName(OperationStatus.Created),
                message: EnumHelper.GetEnumDescription(OperationStatus.Created),
                statusCode: (int)StatusCode.Created,
                data: new ExchangeRateDTO(
                    Id: exchangeRateDomainResult.exchangeRate.Id,
                    BaseCurrency: new CurrencyDTO(
                        exchangeRateDomainResult.exchangeRate.BaseCurrency.Id,
                        exchangeRateDomainResult.exchangeRate.BaseCurrency.Code,
                        exchangeRateDomainResult.exchangeRate.BaseCurrency.FullName,
                        exchangeRateDomainResult.exchangeRate.BaseCurrency.Sign),
                    TargetCurrency: new CurrencyDTO(
                        exchangeRateDomainResult.exchangeRate.TargetCurrency.Id,
                        exchangeRateDomainResult.exchangeRate.TargetCurrency.Code,
                        exchangeRateDomainResult.exchangeRate.TargetCurrency.FullName,
                        exchangeRateDomainResult.exchangeRate.TargetCurrency.Sign),
                    Rate: exchangeRateDomainResult.exchangeRate.Rate)
            );
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
            var dbResult = await exchangeRateRepository.DeleteAsync(id);
    
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