using System.Net;
using CurrencyExchange.API.Mappers;
using CurrencyExchange.Contracts;
using CurrencyExchange.Contracts.Currency.DTOs;
using CurrencyExchange.Contracts.Currency.Requests;
using CurrencyExchange.Contracts.Currency.Responses;
using CurrencyExchange.Domain.Result.Implementations;
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

        IEnumerable<CurrencyResponse> currencyResponse = [];
        
        if (response.Data != null)
            currencyResponse = response.Data.Select(CurrencyMapper.ToCurrencyResponse);

        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
            Domain.Enums.StatusCode.OK => Ok(currencyResponse),
            _ => StatusCode(500, response.Message)
        };
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await currencyCurrencyService.GetByIdAsync(id);

        CurrencyResponse currencyResponse = null!;
        
        if (response.Data != null)
            currencyResponse = CurrencyMapper.ToCurrencyResponse(response.Data);

        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
            Domain.Enums.StatusCode.OK => Ok(currencyResponse),
            _ => StatusCode(500, response.Message)
        };
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCurrencyRequest request)
    {
        var dto = CurrencyMapper.ToCreateCurrencyDTO(request);

        var response = await currencyCurrencyService.CreateAsync(dto);

        CurrencyResponse currencyResponse = null!;
        
        if (response.Data != null)
            currencyResponse = CurrencyMapper.ToCurrencyResponse(response.Data);

        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.UnprocessableEntity => StatusCode(422, response.Errors),
            Domain.Enums.StatusCode.Created => StatusCode(201, currencyResponse),
            Domain.Enums.StatusCode.Conflict => Conflict(response.Message),
            _ => StatusCode(500, response.Message)
        };
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateCurrencyRequest request)
    {
        var dto = CurrencyMapper.ToUpdateCurrencyDTO(request);

        var response = await currencyCurrencyService.UpdateAsync(id, dto);

        CurrencyResponse currencyResponse = null!;
        
        if (response.Data != null)
            currencyResponse = CurrencyMapper.ToCurrencyResponse(response.Data);

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
        var response = await currencyCurrencyService.DeleteAsync(id);

        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
            Domain.Enums.StatusCode.OK => Ok(response.Data),
            _ => StatusCode(500, response.Message)
        };
    }
}