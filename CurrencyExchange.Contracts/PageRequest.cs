using System.ComponentModel;

namespace CurrencyExchange.Contracts;

public record PageRequest(int PageSize, int PageNumber)
{
   [DefaultValue(0)]
   public int PageSize { get; init; } = PageSize;
   [DefaultValue(0)]
   public int PageNumber { get; init; } = PageNumber;
}