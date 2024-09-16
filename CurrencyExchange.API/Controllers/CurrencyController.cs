using CurrencyExchange.Contracts;
using CurrencyExchange.Contracts.Currency;
using CurrencyExchange.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers;

[ApiController]
[Route("currencies")]
public class CurrencyController(ICurrencyService currencyService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
    {
        var response = await currencyService.GetAllAsync(pageRequest.Size, pageRequest.Number);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await currencyService.GetByIdAsync(id);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCurrencyRequest createCurrencyRequest)
    {
        var dto = new CreateCurrencyDTO(
            createCurrencyRequest.Code,
            createCurrencyRequest.FullName,
            createCurrencyRequest.Sign);
        var response = await currencyService.CreateAsync(dto);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateCurrencyRequest updateCurrencyRequest)
    {
        var dto = new UpdateCurrencyDTO(
            updateCurrencyRequest.Code,
            updateCurrencyRequest.FullName,
            updateCurrencyRequest.Sign);
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