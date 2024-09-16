using CurrencyExchange.Domain.Enums;

namespace CurrencyExchange.Domain.Response;

public class BaseResponse<T> : IBaseResponse<T>
{
    /// <summary>
    /// Конструктор для успешного ответа
    /// </summary>
    public BaseResponse(string status, string message, StatusCode statusCode, T data)
    {
        IsSuccess = true;
        Status = status;
        Message = message;
        StatusCode = statusCode;
        Data = data;
    }

    /// <summary>
    /// Конструктор для неуспешного ответа
    /// </summary>
    public BaseResponse(string status, string message, StatusCode statusCode, string[] errors)
    {
        IsSuccess = false;
        Status = status; 
        Message = message;
        StatusCode = statusCode;
        Errors = errors;
    }
    public bool IsSuccess { get; }
    public string Status { get; }
    public string Message { get; }
    public StatusCode StatusCode { get; }
    public T? Data { get; }
    public string[]? Errors { get; }
}