using CurrencyExchange.Contracts.Exchange.DTOs;
using CurrencyExchange.Domain.Response.Interfaces;

namespace CurrencyExchange.Service.Interfaces;

public interface IExchangeService
{
    Task<IBaseResponse<ExchangeDTO>> GetAsync(CreateExchangeDTO dto);
}