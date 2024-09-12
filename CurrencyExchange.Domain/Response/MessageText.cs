namespace CurrencyExchange.Domain.Response;

public class MessageText(string messageText)
{
    public string Message { get; set; } = messageText;
}