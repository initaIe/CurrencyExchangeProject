using CurrencyExchange.DAL.Commons;
using CurrencyExchange.DAL.Repository.Implementations;
using CurrencyExchange.Domain.Entities;
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

// Регистрация CurrencyDAO как реализации интерфейса IBaseDAO
builder.Services.AddScoped<IBaseRepository<>, CurrencyRepository>();

// Регистрация CurrencyRepository как реализации интерфейса IBaseRepository
builder.Services.AddScoped<IBaseRepository<Currency>, CurrencyRepository>();

// Регистрация CurrencyService как реализации интерфейса ICurrencyService
builder.Services.AddScoped<ICurrencyService, CurrencyService>();

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