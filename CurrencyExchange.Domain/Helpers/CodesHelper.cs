using System.Text.RegularExpressions;
using CurrencyExchange.Domain.Models;

namespace CurrencyExchange.Domain.Helpers;

public static class CodesHelper
{
    public static Match GetCodesMath(string codes)
    {
        return Regex.Match(codes, $"^([A-Z]{Currency.CodeLength})([A-Z]{Currency.CodeLength})$");
    }
}