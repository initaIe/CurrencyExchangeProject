using CurrencyExchange.API.Mappers;
using CurrencyExchange.Contracts.ExchangeRateContracts.Requests;
using CurrencyExchange.Contracts.PageContracts;
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

        if (response.Data == null)
            return response.StatusCode switch
            {
                Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
                _ => StatusCode(500, response.Message)
            };

        var exchangeRateResponse
            = response.Data.Select(x => x.ToExchangeRateResponse());
        return Ok(exchangeRateResponse);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await exchangeRateService.GetByIdAsync(id);

        if (response.Data == null)
            return response.StatusCode switch
            {
                Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
                _ => StatusCode(500, response.Message)
            };

        var exchangeRateResponses = response.Data.ToExchangeRateResponse();

        return Ok(exchangeRateResponses);
    }

    [HttpGet("{codes}")]
    public async Task<IActionResult> GetByCodes(string codes)
    {
        var response = await exchangeRateService.GetByCurrencyCodePairAsync(codes);

        if (response.Data == null)
            return response.StatusCode switch
            {
                Domain.Enums.StatusCode.BadRequest => BadRequest(response.Message),
                Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
                _ => StatusCode(500, response.Message)
            };

        var exchangeRateResponses = response.Data.ToExchangeRateResponse();
        return Ok(exchangeRateResponses);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateExchangeRateRequest request)
    {
        var dto = request.ToCreateExchangeRateDTO();

        var response = await exchangeRateService.CreateAsync(dto);

        if (response.Data == null)
            return response.StatusCode switch
            {
                Domain.Enums.StatusCode.UnprocessableEntity => StatusCode(422, response.Errors),
                Domain.Enums.StatusCode.Conflict => Conflict(response.Message),
                _ => StatusCode(500, response.Message)
            };

        var currencyResponse = response.Data.ToExchangeRateResponse();
        return StatusCode(201, currencyResponse);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateExchangeRateRequest request)
    {
        var dto = request.ToUpdateExchangeRateDTO();

        var response = await exchangeRateService.UpdateAsync(id, dto);

        if (response.Data == null)
            return response.StatusCode switch
            {
                Domain.Enums.StatusCode.UnprocessableEntity => StatusCode(422, response.Errors),
                Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
                _ => StatusCode(500, response.Message)
            };

        var currencyResponse = response.Data.ToExchangeRateResponse();
        return Ok(currencyResponse);
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