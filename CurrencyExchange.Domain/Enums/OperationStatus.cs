using System.ComponentModel;

namespace CurrencyExchange.Domain.Enums;

public enum OperationStatus
{
    [Description("The object has been successfully created")]
    Created,
    
    [Description("The object has been successfully received")]
    Received,
    
    [Description("The object has been successfully updated")]
    Updated,
    
    [Description("The object has been successfully deleted")]
    Deleted,
    
    [Description("The operation has failed")]
    Failed
}