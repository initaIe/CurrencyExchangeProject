namespace CurrencyExchange.Domain.Enums;

public enum StatusCode
{
    // Успех.
    OK = 200,

    // Неправильный запрос.
    BadRequest = 400,

    // X не найден.
    NotFound = 404,

    // Такой эелемент уже существует.
    Conflict = 409,

    // Ошибка (например, база данных недоступна).
    InternalServerError = 500
}