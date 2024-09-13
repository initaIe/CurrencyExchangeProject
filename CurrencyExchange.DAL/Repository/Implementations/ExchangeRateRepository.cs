using CurrencyExchange.DAL.Commons;
using CurrencyExchange.DAL.DTOs;
using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.DAL.Result;
using CurrencyExchange.Domain.Entities;
using Microsoft.Data.Sqlite;

namespace CurrencyExchange.DAL.Repository.Implementations;

public class ExchangeRateRepository(DataBase db)
    : IBaseRepository<ExchangeRate, ExchangeRateDTO>
{
    public async Task<IBaseResult<ExchangeRate>> Create(ExchangeRateDTO dto)
    {
        var commandText = "INSERT INTO ExchangeRates (Id, BaseCurrencyId, TargetCurrencyId, Rate) " +
                          "VALUES (@Id, @BaseCurrencyId, @TargetCurrencyId, @Rate);";

        var parameters = new[]
        {
            new SqliteParameter("@Id", dto.Id),
            new SqliteParameter("@BaseCurrencyId", dto.BaseCurrency.Id),
            new SqliteParameter("@TargetCurrencyId", dto.TargetCurrency.Id),
            new SqliteParameter("@Rate", dto.Rate)
        };

        var affectedRows = await db.ExecuteAsync(commandText, parameters);

        var isCreated = affectedRows > 0;

        return new BaseResult<ExchangeRate>
        {
            IsSuccess = isCreated,
            Message = isCreated
                ? $"ExchangeRate with Id {dto.Id} has been created"
                : $"ExchangeRate with Id {dto.Id} has not been created"
        };
    }

    public async Task<IBaseResult<ExchangeRate>> GetById(Guid id)
    {
        var commandText = "SELECT " +
                          "er.Id AS ExchangeRateId, " +
                          "bc.Id AS BaseCurrencyId, " +
                          "bc.Code AS BaseCurrencyCode, " +
                          "bc.FullName AS BaseCurrencyFullName, " +
                          "bc.Sign AS BaseCurrencySign, " +
                          "tc.Id AS TargetCurrencyId, " +
                          "tc.Code AS TargetCurrencyCode, " +
                          "tc.FullName AS TargetCurrencyFullName, " +
                          "tc.Sign AS TargetCurrencySign, er.Rate " +
                          "FROM  ExchangeRates er " +
                          "JOIN  Currencies bc ON er.BaseCurrencyId = bc.Id " +
                          "JOIN Currencies tc ON er.TargetCurrencyId = tc.Id " +
                          "WHERE  er.Id = @Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        var exchangeRateResult = await db.QuerySingleOrDefaultAsync(
            commandText,
            reader =>
            {
                // Создаем базовую валюту
                var baseCurrencyResult = Currency.Create(
                    reader.GetGuid(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4)
                );

                // Создаем целевую валюту
                var targetCurrencyResult = Currency.Create(
                    reader.GetGuid(5),
                    reader.GetString(6),
                    reader.GetString(7),
                    reader.GetString(8)
                );

                // Если одна из валют не создана, возвращаем null
                if (baseCurrencyResult.currency == null || targetCurrencyResult.currency == null)
                    return (null, baseCurrencyResult.error + targetCurrencyResult.error);

                // Создаем ExchangeRate, если обе валюты валидны
                return ExchangeRate.Create(
                    reader.GetGuid(0),
                    baseCurrencyResult.currency,
                    targetCurrencyResult.currency,
                    reader.GetDecimal(9)
                );
            },
            parameters
        );

        var isReceived = exchangeRateResult.exchangeRate != null;

        // Формируем сообщение с учетом результата
        return new BaseResult<ExchangeRate>
        {
            IsSuccess = isReceived,
            Message = isReceived
                ? $"ExchangeRate with Id {id} was successfully received."
                : $"ExchangeRate with Id {id} was not received.",
            Data = isReceived ? exchangeRateResult.exchangeRate : null
        };
    }

    public async Task<IBaseResult<IEnumerable<ExchangeRate>>> GetAll(int limit = 0, int offset = 0)
    {
        var commandText = "SELECT * FROM ExchangeRates";
        var parameters = Array.Empty<SqliteParameter>();

        if (limit > 0 && offset > 0)
        {
            var currentOffset = (offset - 1) * limit;
            commandText += " LIMIT @Limit OFFSET @Offset";

            parameters = new[]
            {
                new SqliteParameter("@Limit", limit),
                new SqliteParameter("@Offset", currentOffset)
            };
        }

        var exchangeRatesCreationResult = await db.QueryAsync(
            commandText,
            reader =>
            {
                var baseCurrencyResult = Currency.Create(
                    reader.GetGuid(1),
                    reader.GetString(4),
                    reader.GetString(5),
                    reader.GetString(6));

                var targetCurrencyResult = Currency.Create(
                    reader.GetGuid(2),
                    reader.GetString(7),
                    reader.GetString(8),
                    reader.GetString(9));

                if (baseCurrencyResult.currency == null || targetCurrencyResult.currency == null)
                    return (null, baseCurrencyResult.error + targetCurrencyResult.error);

                return ExchangeRate.Create(
                    reader.GetGuid(0),
                    baseCurrencyResult.currency,
                    targetCurrencyResult.currency,
                    reader.GetDecimal(3));
            },
            parameters
        );

        var exchangeRatesList = exchangeRatesCreationResult.ToList();

        var isReceived = exchangeRatesList.Count > 0;

        var errors = exchangeRatesList
            .Where(c => !string.IsNullOrEmpty(c.error))
            .Select(c => c.error)
            .ToList();

        var validExchangeRates = exchangeRatesList
            .Where(c => c.exchangeRate != null)
            .Select(c => c.exchangeRate!)
            .ToList();

        var errorMessage = errors.Count > 0
            ? $"Some exchange rates were not created due to errors: {string.Join("; ", errors)}"
            : null;

        return new BaseResult<IEnumerable<ExchangeRate>>
        {
            IsSuccess = isReceived && errors.Count == 0,
            Message = isReceived
                ? errorMessage
                  ?? "All exchange rates were successfully received"
                : "Not all exchange rates were received",
            Data = validExchangeRates.Count != 0 ? validExchangeRates : null
        };
    }

    public async Task<IBaseResult<ExchangeRate>> Delete(Guid id)
    {
        var commandText = "DELETE FROM ExchangeRates " +
                          "WHERE Id=@Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        var affectedRows = await db.ExecuteAsync(commandText, parameters);

        var isDeleted = affectedRows > 0;

        return new BaseResult<ExchangeRate>
        {
            IsSuccess = isDeleted,
            Message = isDeleted
                ? $"ExchangeRate with Id {id} has been deleted"
                : $"ExchangeRate with Id {id} has not been deleted"
        };
    }

    public async Task<IBaseResult<ExchangeRate>> Update(Guid id, ExchangeRateDTO dto)
    {
        var commandText = "UPDATE ExchangeRates " +
                          "SET Id = @NewId, " +
                          "BaseCurrencyId = @BaseCurrencyId, " +
                          "TargetCurrencyId = @TargetCurrencyId, " +
                          "Rate = @Rate " +
                          "WHERE Id = @OldId;";

        var parameters = new[]
        {
            new SqliteParameter("@OldId", id),
            new SqliteParameter("@NewId", dto.Id),
            new SqliteParameter("@BaseCurrencyId", dto.BaseCurrency.Id),
            new SqliteParameter("@TargetCurrencyId", dto.TargetCurrency.Id),
            new SqliteParameter("@Rate", dto.Rate)
        };

        var affectedRows = await db.ExecuteAsync(commandText, parameters);

        var isUpdated = affectedRows > 0;

        return new BaseResult<ExchangeRate>
        {
            IsSuccess = isUpdated,
            Message = isUpdated
                ? $"ExchangeRate with Id {dto.Id} has been created"
                : $"ExchangeRate with Id {id} has not been created"
        };
    }
}