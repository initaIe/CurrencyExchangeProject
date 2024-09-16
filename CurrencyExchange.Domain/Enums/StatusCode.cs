using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CurrencyExchange.Domain.Enums;

public enum StatusCode
{
    // Успех
    [Display(Name = "OK")]
    [Description("The request was successful")]
    OK = 200,

    // Успех при создании
    [Display(Name = "Created")]
    [Description("The request was successful, and a new resource has been created")]
    Created = 201,

    // Неправильный запрос
    [Display(Name = "BadRequest")]
    [Description("The request was malformed or invalid")]
    BadRequest = 400,

    // X не найден
    [Display(Name = "NotFound")]
    [Description("The requested resource was not found")]
    NotFound = 404,

    // Такой элемент уже существует и тд.
    [Display(Name = "Conflict")]
    [Description("A conflict occurred")]
    Conflict = 409,

    // Ошибка сервера
    [Display(Name = "InternalServerError")]
    [Description("An internal server error occurred")]
    InternalServerError = 500
}