using CurrencyExchange.Domain.Enums;
using CurrencyExchange.Domain.Helpers;
using CurrencyExchange.Domain.Response.Interfaces;

namespace CurrencyExchange.Domain.Response.Implementations;

public class FailedErrorResponse(
    int statusCode,
    List<string> errors,
    string? status = null,
    string? message = null)
    : IErrorResponse
{
    public string Status { get; } = status ?? EnumHelper.GetEnumDisplayName(OperationStatus.Failed);
    public string Message { get; } = message ?? EnumHelper.GetEnumDescription(OperationStatus.Failed);
    public int StatusCode { get; } = statusCode;
    public List<string> Errors { get; } = errors;
}