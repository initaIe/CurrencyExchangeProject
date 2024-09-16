using CurrencyExchange.Domain.Enums;

namespace CurrencyExchange.Domain.Result;

public record BaseResult<T> : IBaseResult<T>
{
    /// <summary>
    /// Конструктор для успешного результата
    /// </summary>
    public BaseResult(OperationStatus operationStatus, T data)
    {
        IsSuccess = true;
        Status = operationStatus;
        Data = data;
    }
    
    /// <summary>
    /// Конструктор для неуспешного результата с сообщением об ошибке
    /// </summary>
    public BaseResult(OperationStatus operationStatus)
    {
        IsSuccess = false;
        Status = operationStatus;
    }
    
    public bool IsSuccess { get; }
    public OperationStatus Status { get; }
    public T? Data { get; }
}