using CurrencyExchange.Contracts.Currency.DTOs;
using CurrencyExchange.Contracts.ExchangeRate.DTOs;
using CurrencyExchange.DAL.Entities;
using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Enums;
using CurrencyExchange.Domain.Helpers;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Response.Implementations;
using CurrencyExchange.Domain.Response.Interfaces;
using CurrencyExchange.Service.Factories;
using CurrencyExchange.Service.Interfaces;
using CurrencyExchange.Service.Mappers;

namespace CurrencyExchange.Service.Implementations;

public class ExchangeRateService(
    IRepository<ExchangeRate, ExchangeRateEntity> exchangeRateRepository,
    ExchangeRateFactory exchangeRateFactory)
    : IExchangeRateService
{
    public async Task<IBaseResponse<ExchangeRateDTO>> CreateAsync(CreateExchangeRateDTO dto)
    {
        try
        {
            var domainModelResult = await exchangeRateFactory.Create(
                null,
                dto.BaseCurrencyId,
                dto.TargetCurrencyId,
                dto.Rate);
            
            if (!domainModelResult.IsSuccess || domainModelResult.Data == null)
                return BaseResponse<ExchangeRateDTO>.UnprocessableEntity(domainModelResult.Errors);

            var dbResult = await exchangeRateRepository.CreateAsync(domainModelResult.Data);
            
            if (!dbResult.IsSuccess || dbResult.Data == null)
                return BaseResponse<ExchangeRateDTO>.Conflict();
            
            var exchangeRateDTO = ExchangeRateMapper.ToExchangeRateDTO(dbResult.Data);
            
            return BaseResponse<ExchangeRateDTO>.Created(exchangeRateDTO);
        }
        catch (Exception ex)
        {
            return BaseResponse<ExchangeRateDTO>.InternalServerError();
        }
    }

    public async Task<IBaseResponse<IEnumerable<ExchangeRateDTO>>> GetAllAsync(int pageSize, int pageNumber)
    {
        try
        {
            var dbResult = await exchangeRateRepository.GetAllAsync(pageSize, pageNumber);

            if (!dbResult.IsSuccess || dbResult.Data == null)
                return BaseResponse<IEnumerable<ExchangeRateDTO>>.NotFound();
            
            var dto = dbResult.Data.Select(ExchangeRateMapper.ToExchangeRateDTO);

            return BaseResponse<IEnumerable<ExchangeRateDTO>>.Ok(dto);
        }
        catch (Exception ex)
        {
            return BaseResponse<IEnumerable<ExchangeRateDTO>>.InternalServerError();
        }
    }

    public async Task<IBaseResponse<ExchangeRateDTO>> GetByIdAsync(Guid id)
    {
        try
        {
            var dbResult = await exchangeRateRepository.GetByIdAsync(id);

            if (!dbResult.IsSuccess || dbResult.Data == null)
                return BaseResponse<ExchangeRateDTO>.NotFound();
            
            var dto = ExchangeRateMapper.ToExchangeRateDTO(dbResult.Data);
            
            return BaseResponse<ExchangeRateDTO>.Ok(dto);
        }
        catch (Exception ex)
        {
            return BaseResponse<ExchangeRateDTO>.InternalServerError();
        }
    }

    public async Task<IBaseResponse<ExchangeRateDTO>> UpdateAsync(Guid id, UpdateExchangeRateDTO dto)
    {
        try
        {
            var domainModelResult = await exchangeRateFactory.Create(
                id,
                dto.BaseCurrencyId,
                dto.TargetCurrencyId,
                dto.Rate);
            
            if (!domainModelResult.IsSuccess || domainModelResult.Data == null)
                return BaseResponse<ExchangeRateDTO>.UnprocessableEntity(domainModelResult.Errors);
            
            var dbResult = await exchangeRateRepository.UpdateAsync(id, domainModelResult.Data);
            
            if (!dbResult.IsSuccess || dbResult.Data == null)
                return BaseResponse<ExchangeRateDTO>.Conflict();
            
            var exchangeRateDTO = ExchangeRateMapper.ToExchangeRateDTO(dbResult.Data);
            
            return BaseResponse<ExchangeRateDTO>.Ok(exchangeRateDTO);
        }
        catch (Exception ex)
        {
            return BaseResponse<ExchangeRateDTO>.InternalServerError();
        }
    }

    public async Task<IBaseResponse<Guid>> DeleteAsync(Guid id)
    {
        try
        {
            var dbResult = await exchangeRateRepository.DeleteAsync(id);

            if (!dbResult.IsSuccess)
                return BaseResponse<Guid>.NotFound();

            return BaseResponse<Guid>.Ok(id);
        }
        catch (Exception ex)
        {
            return BaseResponse<Guid>.InternalServerError();
        }
    }
}