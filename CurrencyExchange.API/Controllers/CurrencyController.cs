using CurrencyExchange.API.Mappers;
using CurrencyExchange.Contracts.CurrencyContracts.Requests;
using CurrencyExchange.Contracts.PageContracts;
using CurrencyExchange.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers;

[ApiController]
[Route("currencies")]
public class CurrencyController(ICurrencyService currencyCurrencyService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PageRequest request)
    {
        var response = await currencyCurrencyService.GetAllAsync(request.Size, request.Number);

        if (response.Data == null)
            return response.StatusCode switch
            {
                Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
                _ => StatusCode(500, response.Message)
            };

        var currencyResponse = response.Data
            .Select(x => x.ToCurrencyResponse());
        return Ok(currencyResponse);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await currencyCurrencyService.GetByIdAsync(id);

        if (response.Data == null)
            return response.StatusCode switch
            {
                Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
                _ => StatusCode(500, response.Message)
            };

        var currencyResponse = response.Data.ToCurrencyResponse();
        return Ok(currencyResponse);
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        var response = await currencyCurrencyService.GetByCodeAsync(code);

        if (response.Data == null)
            return response.StatusCode switch
            {
                Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
                _ => StatusCode(500, response.Message)
            };

        var currencyResponse = response.Data.ToCurrencyResponse();
        return Ok(currencyResponse);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCurrencyRequest request)
    {
        var dto = request.ToCreateCurrencyDTO();

        var response = await currencyCurrencyService.CreateAsync(dto);

        if (response.Data == null)
            return response.StatusCode switch
            {
                Domain.Enums.StatusCode.UnprocessableEntity => StatusCode(422, response.Errors),
                Domain.Enums.StatusCode.Conflict => Conflict(response.Message),
                _ => StatusCode(500, response.Message)
            };

        var currencyResponse = response.Data.ToCurrencyResponse();
        return StatusCode(201, currencyResponse);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateCurrencyRequest request)
    {
        var dto = request.ToUpdateCurrencyDTO();

        var response = await currencyCurrencyService.UpdateAsync(id, dto);

        if (response.Data == null)
            return response.StatusCode switch
            {
                Domain.Enums.StatusCode.UnprocessableEntity => StatusCode(422, response.Errors),
                Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
                _ => StatusCode(500, response.Message)
            };

        var currencyResponse = response.Data.ToCurrencyResponse();
        return Ok(currencyResponse);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await currencyCurrencyService.DeleteAsync(id);

        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
            Domain.Enums.StatusCode.OK => Ok(response.Data),
            _ => StatusCode(500, response.Message)
        };
    }
}