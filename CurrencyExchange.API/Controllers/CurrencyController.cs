using CurrencyExchange.API.Mappers;
using CurrencyExchange.Contracts;
using CurrencyExchange.Contracts.Currency;
using CurrencyExchange.Contracts.Currency.DTOs;
using CurrencyExchange.Contracts.Currency.Requests;
using CurrencyExchange.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers;

[ApiController]
[Route("currencies")]
public class CurrencyController(IService<CreateCurrencyDTO, UpdateCurrencyDTO> currencyService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PageRequest request)
    {
        var response = await currencyService.GetAllAsync(request.Size, request.Number);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await currencyService.GetByIdAsync(id);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCurrencyRequest request)
    {
        var dto = CurrencyMapper.ToCreateCurrencyDTO(request);
        var response = await currencyService.CreateAsync(dto);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateCurrencyRequest request)
    {
        var dto = CurrencyMapper.ToUpdateCurrencyDTO(request);
        var response = await currencyService.UpdateAsync(id, dto);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await currencyService.DeleteAsync(id);
        return StatusCode(response.StatusCode, response);
    }
}