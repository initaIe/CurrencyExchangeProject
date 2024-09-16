using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CurrencyExchange.Domain.Enums;

public enum OperationStatus
{
    [Display(Name = "Created")] [Description("Successfully created")]
    Created,

    [Display(Name = "Received")] [Description("Successfully received")]
    Received,

    [Display(Name = "Updated")] [Description("Successfully updated")]
    Updated,

    [Display(Name = "Deleted")] [Description("Successfully deleted")]
    Deleted,

    [Display(Name = "Failed")] [Description("Operation was failed")]
    Failed
}