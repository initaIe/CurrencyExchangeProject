using CurrencyExchange.API.Mappers;
using CurrencyExchange.Contracts.ExchangeRate.Requests;
using CurrencyExchange.Contracts.ExchangeRate.Responses;
using CurrencyExchange.Contracts.Page;
using CurrencyExchange.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers;

[ApiController]
[Route("exchange-rates")]
public class ExchangeRateController(IExchangeRateService exchangeRateService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
    {
        var response = await exchangeRateService.GetAllAsync(pageRequest.Size, pageRequest.Number);

        IEnumerable<ExchangeRateResponse> exchangeRateResponses = [];

        if (response.Data != null)
            exchangeRateResponses = response.Data.Select(ExchangeRateMapper.ToExchangeRateResponse);

        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
            Domain.Enums.StatusCode.OK => Ok(exchangeRateResponses),
            _ => StatusCode(500, response.Message)
        };
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await exchangeRateService.GetByIdAsync(id);

        ExchangeRateResponse exchangeRateResponses = null!;

        if (response.Data != null)
            exchangeRateResponses = ExchangeRateMapper.ToExchangeRateResponse(response.Data);

        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
            Domain.Enums.StatusCode.OK => Ok(exchangeRateResponses),
            _ => StatusCode(500, response.Message)
        };
    }

    [HttpGet("{codes}")]
    public async Task<IActionResult> GetByCodes(string codes)
    {
        var response = await exchangeRateService.GetByCurrencyCodePairAsync(codes);

        ExchangeRateResponse exchangeRateResponses = null!;

        if (response.Data != null)
            exchangeRateResponses = ExchangeRateMapper.ToExchangeRateResponse(response.Data);

        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.BadRequest => BadRequest(response.Message),
            Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
            Domain.Enums.StatusCode.OK => Ok(exchangeRateResponses),
            _ => StatusCode(500, response.Message)
        };
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateExchangeRateRequest request)
    {
        var dto = ExchangeRateMapper.ToCreateExchangeRateDTO(request);

        var response = await exchangeRateService.CreateAsync(dto);

        ExchangeRateResponse currencyResponse = null!;

        if (response.Data != null)
            currencyResponse = ExchangeRateMapper.ToExchangeRateResponse(response.Data);

        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.UnprocessableEntity => StatusCode(422, response.Errors),
            Domain.Enums.StatusCode.Created => StatusCode(201, currencyResponse),
            Domain.Enums.StatusCode.Conflict => Conflict(response.Message),
            _ => StatusCode(500, response.Message)
        };
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateExchangeRateRequest request)
    {
        var dto = ExchangeRateMapper.ToUpdateExchangeRateDTO(request);

        var response = await exchangeRateService.UpdateAsync(id, dto);

        ExchangeRateResponse currencyResponse = null!;

        if (response.Data != null)
            currencyResponse = ExchangeRateMapper.ToExchangeRateResponse(response.Data);

        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.UnprocessableEntity => StatusCode(422, response.Errors),
            Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
            Domain.Enums.StatusCode.Created => Ok(currencyResponse),
            _ => StatusCode(500, response.Message)
        };
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await exchangeRateService.DeleteAsync(id);

        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
            Domain.Enums.StatusCode.OK => Ok(response.Data),
            _ => StatusCode(500, response.Message)
        };
    }
}