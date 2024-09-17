using CurrencyExchange.Contracts;
using CurrencyExchange.Contracts.Currency;
using CurrencyExchange.Contracts.Currency.DTOs;
using CurrencyExchange.Contracts.ExchangeRate;
using CurrencyExchange.Contracts.ExchangeRate.DTOs;
using CurrencyExchange.Contracts.ExchangeRate.Requests;
using CurrencyExchange.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers;

[ApiController]
[Route("exchange-rates")]
public class ExchangeRateController(IService<CreateExchangeRateDTO, UpdateExchangeRateDTO> exchangeRateService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
    {
        var response = await exchangeRateService.GetAllAsync(pageRequest.Size, pageRequest.Number);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await exchangeRateService.GetByIdAsync(id);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateExchangeRateRequest request)
    {
        var dto = new CreateExchangeRateDTO(
            request.BaseCurrencyId,
            request.TargetCurrencyId,
            request.Rate);
        
        var response = await exchangeRateService.CreateAsync(dto);
        
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateExchangeRateRequest request)
    {
        var dto = new UpdateExchangeRateDTO(
            request.BaseCurrencyId,
            request.TargetCurrencyId,
            request.Rate);
        
        var response = await exchangeRateService.UpdateAsync(id, dto);
        
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await exchangeRateService.DeleteAsync(id);
        return StatusCode(response.StatusCode, response);
    }
}