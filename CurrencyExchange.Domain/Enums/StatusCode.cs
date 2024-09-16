using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CurrencyExchange.Domain.Enums;

public enum StatusCode
{
    [Display(Name = "OK")] [Description("The request was successful")]
    OK = 200,

    [Display(Name = "Created")] [Description("The request was successful, and a new resource has been created")]
    Created = 201,

    // No Content
    [Display(Name = "Deleted")] [Description("The request was successful, and resource has been deleted")]
    Deleted = 204,

    [Display(Name = "BadRequest")] [Description("The request was malformed or invalid")]
    BadRequest = 400,

    [Display(Name = "NotFound")] [Description("The requested resource was not found")]
    NotFound = 404,

    [Display(Name = "Conflict")] [Description("A conflict occurred")]
    Conflict = 409,

    [Display(Name = "Unprocessable Entity")] [Description("A unprocessable entity occurred")]
    UnprocessableEntity = 422,

    [Display(Name = "InternalServerError")] [Description("An internal server error occurred")]
    InternalServerError = 500
}