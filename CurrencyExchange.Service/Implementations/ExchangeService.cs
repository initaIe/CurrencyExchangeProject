using CurrencyExchange.Contracts.Exchange.DTOs;
using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Helpers;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Response.Implementations;
using CurrencyExchange.Domain.Response.Interfaces;
using CurrencyExchange.Domain.Result.Implementations;
using CurrencyExchange.Service.Interfaces;
using CurrencyExchange.Service.Mappers;

namespace CurrencyExchange.Service.Implementations;

public class ExchangeService(
    ICurrencyRepository currencyRepository,
    IExchangeRateRepository exchangeRateRepository)
    : IExchangeService
{
    public async Task<IBaseResponse<ExchangeDTO>> GetAsync(CreateExchangeDTO dto)
    {
        var optimalRateResult = await GetOptimalRateAsync(dto.FromCode, dto.ToCode);

        if (!optimalRateResult.IsSuccess)
            return BaseResponse<ExchangeDTO>.NotFound();
        
        var fromCurrencyDbResult = await currencyRepository.GetByCodeAsync(dto.FromCode);
        
        if (!fromCurrencyDbResult.IsSuccess)
            return BaseResponse<ExchangeDTO>.NotFound();
        
        var toCurrencyDbResult = await currencyRepository.GetByCodeAsync(dto.ToCode);
        
        if (!toCurrencyDbResult.IsSuccess)
            return BaseResponse<ExchangeDTO>.NotFound();

        var fromCurrencyDTO = CurrencyMapper.ToCurrencyDTO(fromCurrencyDbResult.Data!); 
        
        var toCurrencyDTO = CurrencyMapper.ToCurrencyDTO(toCurrencyDbResult.Data!); 
        
        var exchangeDTO = new ExchangeDTO(fromCurrencyDTO, toCurrencyDTO, optimalRateResult.Data!, dto.Amount);

        return BaseResponse<ExchangeDTO>.Ok(exchangeDTO);
    }

    public async Task<Result<decimal>> GetOptimalRateAsync(string fromCode, string toCode)
    {
        var directRateResult = await GetDirectRateAsync(fromCode, toCode);

        if (directRateResult.IsSuccess)
            return Result<decimal>.Success(directRateResult.Data);

        var reverseRateResult = await GetReverseRateAsync(fromCode, toCode);

        if (reverseRateResult.IsSuccess)
            return Result<decimal>.Success(reverseRateResult.Data);

        var crossRateResult = await GetCrossRateAsync(fromCode, toCode);

        if (crossRateResult.IsSuccess)
            return Result<decimal>.Success(crossRateResult.Data);

        return Result<decimal>.Failure();
    }

    public async Task<Result<decimal>> GetDirectRateAsync(string fromCode, string toCode)
    {
        var directExchangeRateDbResult = await exchangeRateRepository.GetByCodePairAsync(
            fromCode,
            toCode);

        if (!directExchangeRateDbResult.IsSuccess)
            return Result<decimal>.Failure();

        var directRate = directExchangeRateDbResult.Data!.Rate;

        return Result<decimal>.Success(directRate);
    }

    public async Task<Result<decimal>> GetReverseRateAsync(string fromCode, string toCode)
    {
        var reverseExchangeRateDbResult = await exchangeRateRepository.GetByCodePairAsync(
            toCode,
            fromCode);

        if (!reverseExchangeRateDbResult.IsSuccess)
            return Result<decimal>.Failure();

        var reverseRate = RateConverter.GetFromReverseRate(reverseExchangeRateDbResult.Data!.Rate);

        return Result<decimal>.Success(reverseRate);
    }

    public async Task<Result<decimal>> GetCrossRateAsync(string fromCode, string toCode)
    {
        const string baseCurrencyCode = "USD";

        var currentCurrencyToBaseCurrency =
            await exchangeRateRepository.GetByCodePairAsync(fromCode, baseCurrencyCode);

        var targetCurrencyToBase =
            await exchangeRateRepository.GetByCodePairAsync(toCode, baseCurrencyCode);

        if (!currentCurrencyToBaseCurrency.IsSuccess || !targetCurrencyToBase.IsSuccess)
            return Result<decimal>.Failure();

        var crossRate = RateConverter.GetFromCrossRates(currentCurrencyToBaseCurrency.Data!.Rate,
            targetCurrencyToBase.Data!.Rate);

        return Result<decimal>.Success(crossRate);
    }
}