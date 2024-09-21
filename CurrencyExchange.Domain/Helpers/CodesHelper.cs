using System.Text.RegularExpressions;

namespace CurrencyExchange.Domain.Helpers;

public static class CodesHelper
{
    public static Match GetCodesMath(string codes)
    {
        return Regex.Match(codes, @"^([A-Z]{3})([A-Z]{3})$");
    }
}