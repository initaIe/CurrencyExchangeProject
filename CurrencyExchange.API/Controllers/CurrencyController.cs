using CurrencyExchange.Domain.Response;
using CurrencyExchange.Service.DTOs;
using CurrencyExchange.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CurrencyController(ICurrencyService currencyService) : ControllerBase
{
    [HttpGet("All")]
    public async Task<ActionResult<IBaseResponse<IEnumerable<CurrencyDTO>>>> GetCurrencies()
    {
        var response = await currencyService.GetCurrenciesAsync();

        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.InternalServerError => StatusCode(500, response.Message),
            _ => Ok(response.Data)
        };
    }

    [HttpGet("Paged")]
    public async Task<ActionResult<IBaseResponse<IEnumerable<CurrencyDTO>>>> GetCurrencies
        (int pageSize, int pageNumber)
    {
        var response = await currencyService.GetCurrenciesAsync(pageSize, pageNumber);

        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.InternalServerError => StatusCode(500, response.Message),
            _ => Ok(response.Data)
        };
    }

    [HttpPost]
    [Route("Update")]
    public async Task<ActionResult<IBaseResponse<IEnumerable<CurrencyDTO>>>> UpdateCurrency
        (int id, CurrencyDTO updatedCurrency)
    {
        var response = await currencyService.UpdateCurrencyAsync(id, updatedCurrency);

        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.InternalServerError => StatusCode(500, response.Message),
            Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
            Domain.Enums.StatusCode.BadRequest => BadRequest(response.Message),
            _ => Ok(response.Message)
        };
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<ActionResult<IBaseResponse<IEnumerable<CurrencyDTO>>>> DeleteCurrency(int id)
    {
        var response = await currencyService.DeleteCurrencyAsync(id);

        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
            Domain.Enums.StatusCode.BadRequest => BadRequest(response.Message),
            Domain.Enums.StatusCode.InternalServerError => StatusCode(500, response.Message),
            _ => Ok(response.Message)
        };
    }

    [HttpGet]
    [Route("Get")]
    public async Task<ActionResult<IBaseResponse<IEnumerable<CurrencyDTO>>>> GetCurrency(int id)
    {
        var response = await currencyService.GetCurrencyByIdAsync(id);

        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
            Domain.Enums.StatusCode.InternalServerError => StatusCode(500, response.Message),
            _ => Ok(response.Data)
        };
    }

    [HttpPut]
    [Route("Create")]
    public async Task<ActionResult<IBaseResponse<IEnumerable<CurrencyDTO>>>> CreateCurrency
        (CurrencyDTO updatedCurrency)
    {
        var response = await currencyService.CreateCurrencyAsync(updatedCurrency);

        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.InternalServerError => StatusCode(500, response.Message),
            Domain.Enums.StatusCode.Conflict => Conflict(response.Message),
            _ => Ok(response.Message)
        };
    }
}