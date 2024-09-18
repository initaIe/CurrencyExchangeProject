using CurrencyExchange.Domain.Enums;
using CurrencyExchange.Domain.Helpers;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Response.Interfaces;

namespace CurrencyExchange.Domain.Response.Implementations;

public class BaseResponse<T> : IBaseResponse<T>
{
    private BaseResponse(string? message, StatusCode statusCode, T? data, List<string>? errors)
    {
        Message = message ?? null;
        StatusCode = statusCode;
        Data = data ?? default;
        Errors = errors ?? [];
    }
    
    public string? Message { get; }
    public StatusCode StatusCode { get; }
    public T? Data { get; }
    public List<string> Errors { get; } 
    

    public static BaseResponse<T> Created(T data)
    {
        return new BaseResponse<T>(
            EnumHelper.GetEnumDescription(StatusCode.Created),
            StatusCode.Created,
            data,
            null);
    }
    
    public static BaseResponse<T> OkNoContent()
    {
        return new BaseResponse<T>(
            null,
            StatusCode.OkNoContent,
            default,
            null);
    }
    
    public static BaseResponse<T> Ok(T data)
    {
        return new BaseResponse<T>(
            EnumHelper.GetEnumDescription(StatusCode.OK),
            StatusCode.OK,
            data,
            null);
    }
    
    public static BaseResponse<T> BadRequest()
    {
        return new BaseResponse<T>(
            EnumHelper.GetEnumDescription(StatusCode.BadRequest),
            StatusCode.BadRequest,
            default,
            null);
    }
    
    public static BaseResponse<T> InternalServerError()
    {
        return new BaseResponse<T>(
            EnumHelper.GetEnumDescription(StatusCode.InternalServerError),
            StatusCode.InternalServerError,
            default,
            null);
    }
    public static BaseResponse<T> UnprocessableEntity(List<string> errors)
    {
        return new BaseResponse<T>(
            EnumHelper.GetEnumDescription(StatusCode.UnprocessableEntity),
            StatusCode.UnprocessableEntity,
            default,
            errors);
    }
    
    public static BaseResponse<T> Conflict()
    {
        return new BaseResponse<T>(
            EnumHelper.GetEnumDescription(StatusCode.Conflict),
            StatusCode.Conflict,
            default,
            null);
    }
    
    public static BaseResponse<T> NotFound()
    {
        return new BaseResponse<T>(
            EnumHelper.GetEnumDescription(StatusCode.NotFound),
            StatusCode.NotFound,
            default,
            null);
    }
}