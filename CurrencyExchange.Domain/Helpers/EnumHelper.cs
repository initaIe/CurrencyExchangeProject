using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CurrencyExchange.Domain.Helpers;

public static class EnumHelper
{
    public static string GetEnumDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attribute == null ? value.ToString() : attribute.Description;
    }

    public static string GetEnumDisplayName(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = (DisplayAttribute)Attribute.GetCustomAttribute(field, typeof(DisplayAttribute));
        return attribute == null ? value.ToString() : attribute.Name;
    }
}