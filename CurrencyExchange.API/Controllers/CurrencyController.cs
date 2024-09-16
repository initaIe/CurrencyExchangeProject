using CurrencyExchange.Contracts;
using CurrencyExchange.Contracts.Currency;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Response;
using CurrencyExchange.Service.DTOs;
using CurrencyExchange.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CurrencyController(ICurrencyService currencyService) : ControllerBase
{
    
}