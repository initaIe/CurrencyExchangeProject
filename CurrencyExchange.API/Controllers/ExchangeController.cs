using CurrencyExchange.API.Mappers;
using CurrencyExchange.Contracts.ExchangeContracts.Requests;
using CurrencyExchange.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers;

[ApiController]
[Route("exchange")]
public class ExchangeController(IExchangeService exchangeService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Exchange([FromQuery] GetExchangeRequest request)
    {
        var dto = request.ToGetExchangeDTO();

        var response = await exchangeService.GetAsync(dto);

        if (response.Data == null)
            return response.StatusCode switch
            {
                Domain.Enums.StatusCode.NotFound => NotFound(response.Message),
                _ => StatusCode(500, response.Message)
            };

        var exchangeResponse = response.Data.ToExchangeResponse();
        return Ok(exchangeResponse);
    }
}