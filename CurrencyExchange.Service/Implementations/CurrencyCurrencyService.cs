using CurrencyExchange.Contracts.CurrencyContracts.DTOs;
using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Response.Implementations;
using CurrencyExchange.Domain.Response.Interfaces;
using CurrencyExchange.Service.Factories;
using CurrencyExchange.Service.Interfaces;
using CurrencyExchange.Service.Mappers;

namespace CurrencyExchange.Service.Implementations;

public class CurrencyCurrencyService(
    ICurrencyRepository currencyRepository,
    CurrencyFactory currencyFactory)
    : ICurrencyService
{
    public async Task<IBaseResponse<CurrencyDTO>> CreateAsync(CreateCurrencyDTO dto)
    {
        try
        {
            var factoryResult = currencyFactory.Create(dto.Code, dto.FullName, dto.Sign);

            if (!factoryResult.IsSuccess)
                return BaseResponse<CurrencyDTO>.UnprocessableEntity(factoryResult.Errors!);

            var dbResult = await currencyRepository.CreateAsync(factoryResult.Data!);

            if (!dbResult.IsSuccess)
                return BaseResponse<CurrencyDTO>.Conflict();

            var currencyDTO = CurrencyMapper.ToCurrencyDTO(dbResult.Data!);

            return BaseResponse<CurrencyDTO>.Created(currencyDTO);
        }
        catch (Exception ex)
        {
            return BaseResponse<CurrencyDTO>.InternalServerError();
        }
    }

    public async Task<IBaseResponse<IEnumerable<CurrencyDTO>>> GetAllAsync(int pageSize, int pageNumber)
    {
        try
        {
            var dbResult = await currencyRepository.GetAllAsync(pageSize, pageNumber);

            if (!dbResult.IsSuccess)
                return BaseResponse<IEnumerable<CurrencyDTO>>.NotFound();

            var currencyDTOs = dbResult.Data!.Select(CurrencyMapper.ToCurrencyDTO);

            return BaseResponse<IEnumerable<CurrencyDTO>>.Ok(currencyDTOs);
        }
        catch (Exception ex)
        {
            return BaseResponse<IEnumerable<CurrencyDTO>>.InternalServerError();
        }
    }

    public async Task<IBaseResponse<CurrencyDTO>> GetByIdAsync(Guid id)
    {
        try
        {
            var dbResult = await currencyRepository.GetByIdAsync(id);

            if (!dbResult.IsSuccess)
                return BaseResponse<CurrencyDTO>.NotFound();

            var dto = CurrencyMapper.ToCurrencyDTO(dbResult.Data!);

            return BaseResponse<CurrencyDTO>.Ok(dto);
        }
        catch (Exception ex)
        {
            return BaseResponse<CurrencyDTO>.InternalServerError();
        }
    }

    public async Task<IBaseResponse<CurrencyDTO>> GetByCodeAsync(string code)
    {
        try
        {
            var dbResult = await currencyRepository.GetByCodeAsync(code);

            if (!dbResult.IsSuccess)
                return BaseResponse<CurrencyDTO>.NotFound();

            var dto = CurrencyMapper.ToCurrencyDTO(dbResult.Data!);

            return BaseResponse<CurrencyDTO>.Ok(dto);
        }
        catch (Exception ex)
        {
            return BaseResponse<CurrencyDTO>.InternalServerError();
        }
    }

    public async Task<IBaseResponse<CurrencyDTO>> UpdateAsync(Guid id, UpdateCurrencyDTO dto)
    {
        try
        {
            var domainResult = CurrencyMapper.ToCurrency(id, dto);

            if (!domainResult.IsSuccess)
                return BaseResponse<CurrencyDTO>.UnprocessableEntity(domainResult.Errors!);

            var dbResult = await currencyRepository.UpdateAsync(id, domainResult.Data!);

            if (!dbResult.IsSuccess)
                return BaseResponse<CurrencyDTO>.NotFound();

            var currencyDTO = CurrencyMapper.ToCurrencyDTO(dbResult.Data!);

            return BaseResponse<CurrencyDTO>.Ok(currencyDTO);
        }
        catch (Exception ex)
        {
            return BaseResponse<CurrencyDTO>.InternalServerError();
        }
    }

    public async Task<IBaseResponse<Guid>> DeleteAsync(Guid id)
    {
        try
        {
            var dbResult = await currencyRepository.DeleteAsync(id);

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