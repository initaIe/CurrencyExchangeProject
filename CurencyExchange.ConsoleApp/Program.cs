﻿using System.Net;
using System.Text;
 
HttpListener server = new HttpListener();
// установка адресов прослушки
server.Prefixes.Add("http://127.0.0.1:8888/connection/");
server.Start(); // начинаем прослушивать входящие подключения
Console.WriteLine("Сервер запущен. Ожидание подключений...");
 
// получаем контекст
var context = await server.GetContextAsync();
 
var request = context.Request;  // получаем данные запроса
 
Console.WriteLine($"адрес приложения: {request.LocalEndPoint}");
Console.WriteLine($"адрес клиента: {request.RemoteEndPoint}");
Console.WriteLine(request.RawUrl);
Console.WriteLine($"Запрошен адрес: {request.Url}");
Console.WriteLine("Заголовки запроса:");
foreach(string item in request.Headers.Keys)
{
    Console.WriteLine($"{item}:{request.Headers[item]}");
}
 
var response = context.Response;    // получаем объект для установки ответа
byte[] buffer = Encoding.UTF8.GetBytes("Hello METANIT");
// получаем поток ответа и пишем в него ответ
response.ContentLength64 = buffer.Length;
using Stream output = response.OutputStream;
// отправляем данные
await output.WriteAsync(buffer);
await output.FlushAsync();
 
server.Stop(); // останавливаем сервер

Console.Read();