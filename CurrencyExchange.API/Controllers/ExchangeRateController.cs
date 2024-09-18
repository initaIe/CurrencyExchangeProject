using CurrencyExchange.API.Mappers;
using CurrencyExchange.Contracts;
using CurrencyExchange.Contracts.ExchangeRate.DTOs;
using CurrencyExchange.Contracts.ExchangeRate.Requests;
using CurrencyExchange.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers;

[ApiController]
[Route("exchange-rates")]
public class ExchangeRateController(ICurrencyService exchangeRateCurrencyService)
    : ControllerBase
{
    // [HttpGet]
    // public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
    // {
    //     var response = await exchangeRateCurrencyService.GetAllAsync(pageRequest.Size, pageRequest.Number);
    //     return StatusCode(response.StatusCode, response);
    // }
    //
    // [HttpGet("{id:guid}")]
    // public async Task<IActionResult> Get(Guid id)
    // {
    //     var response = await exchangeRateCurrencyService.GetByIdAsync(id);
    //     return StatusCode(response.StatusCode, response);
    // }
    //
    // [HttpPost]
    // public async Task<IActionResult> Create(CreateExchangeRateRequest request)
    // {
    //     var dto = ExchangeRateMapper.ToCreateExchangeRateDTO(request);
    //     var response = await exchangeRateCurrencyService.CreateAsync(dto);
    //     return StatusCode(response.StatusCode, response);
    // }
    //
    // [HttpPut("{id:guid}")]
    // public async Task<IActionResult> Update(Guid id, UpdateExchangeRateRequest request)
    // {
    //     var dto = ExchangeRateMapper.ToUpdateExchangeRateDTO(request);
    //     var response = await exchangeRateCurrencyService.UpdateAsync(id, dto);
    //     return StatusCode(response.StatusCode, response);
    // }
    //
    // [HttpDelete("{id:guid}")]
    // public async Task<IActionResult> Delete(Guid id)
    // {
    //     var response = await exchangeRateCurrencyService.DeleteAsync(id);
    //     return StatusCode(response.StatusCode, response);
    // }
}