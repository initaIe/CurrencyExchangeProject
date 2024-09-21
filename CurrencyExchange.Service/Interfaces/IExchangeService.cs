using CurrencyExchange.Contracts.ExchangeContracts.DTOs;
using CurrencyExchange.Domain.Response.Interfaces;

namespace CurrencyExchange.Service.Interfaces;

public interface IExchangeService
{
    Task<IBaseResponse<ExchangeDTO>> GetAsync(GetExchangeDTO dto);
}