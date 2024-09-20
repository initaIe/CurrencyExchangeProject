using CurrencyExchange.Contracts.Exchange.DTOs;
using CurrencyExchange.Contracts.Exchange.Requests;
using CurrencyExchange.Service.Interfaces;
using CurrencyExchange.Service.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers;

[ApiController]
[Route("exchange")]
public class ExchangeController(IExchangeService exchangeService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Exchange([FromQuery] CreateExchangeRequest request)
    {
        var createExchangeDTO = new CreateExchangeDTO(request.From, request.To, request.Amount);
        var response = await exchangeService.GetAsync(createExchangeDTO);

        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
            Domain.Enums.StatusCode.OK => Ok(response.Data),
            _ => StatusCode(500, response.Message)
        };
    }
}