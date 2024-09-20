using CurrencyExchange.Contracts.Currency.DTOs;

namespace CurrencyExchange.Contracts.Exchange.DTOs;

public record ExchangeDTO
{
    public ExchangeDTO(CurrencyDTO baseCurrency,
        CurrencyDTO targetCurrency,
        decimal rate,
        decimal amount)
    {
        BaseCurrency = baseCurrency;
        TargetCurrency = targetCurrency;
        Rate = rate;
        Amount = amount;
    }

    public CurrencyDTO BaseCurrency { get; }
    public CurrencyDTO TargetCurrency { get; }
    public decimal Rate { get; }
    public decimal Amount { get; }
    public decimal ConvertedAmount => Rate * Amount;
}