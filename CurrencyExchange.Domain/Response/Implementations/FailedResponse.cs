using CurrencyExchange.Domain.Enums;
using CurrencyExchange.Domain.Helpers;
using CurrencyExchange.Domain.Response.Interfaces;

namespace CurrencyExchange.Domain.Response.Implementations;

public class FailedResponse(
    int statusCode,
    string? status = null,
    string? message = null)
    : IResponse
{
    public string Status { get; } = status ?? EnumHelper.GetEnumDisplayName(OperationStatus.Failed);
    public string Message { get; } = message ?? EnumHelper.GetEnumDescription(OperationStatus.Failed);
    public int StatusCode { get; } = statusCode;
}