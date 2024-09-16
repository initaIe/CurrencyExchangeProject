using CurrencyExchange.DAL.Commons;
using CurrencyExchange.DAL.Repository.Implementations;
using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Service.Implementations;
using CurrencyExchange.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<DataBase>(provider =>
{
    var connectionString = provider.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection");
    return new DataBase(connectionString!);
});

builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();

builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();