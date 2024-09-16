using CurrencyExchange.DAL.Commons;
using CurrencyExchange.DAL.Entities;
using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Enums;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Result;
using Microsoft.Data.Sqlite;

namespace CurrencyExchange.DAL.Repository.Implementations;

public class ExchangeRateRepository(DataBase db)
    : IExchangeRateRepository
{
    public async Task<IBaseResult<ExchangeRate>> Create(ExchangeRate exchangeRate)
    {
        var commandText = "INSERT INTO ExchangeRates (Id, BaseCurrencyId, TargetCurrencyId, Rate) " +
                          "VALUES (@Id, @BaseCurrencyId, @TargetCurrencyId, @Rate);";

        var parameters = new[]
        {
            new SqliteParameter("@Id", exchangeRate.Id),
            new SqliteParameter("@BaseCurrencyId", exchangeRate.BaseCurrency.Id),
            new SqliteParameter("@TargetCurrencyId", exchangeRate.TargetCurrency.Id),
            new SqliteParameter("@Rate", exchangeRate.Rate)
        };

        var affectedRows = await db.ExecuteAsync(commandText, parameters);

        var isCreated = affectedRows > 0;

        return isCreated
            ? new BaseResult<ExchangeRate>(
                operationStatus: OperationStatus.Created,
                data: exchangeRate)
            : new BaseResult<ExchangeRate>(
                operationStatus: OperationStatus.Failed);
    }

    public async Task<IBaseResult<ExchangeRateEntity>> GetById(Guid id)
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
                          "tc.Sign AS TargetCurrencySign, " +
                          "er.Rate " +
                          "FROM  ExchangeRates er " +
                          "JOIN  Currencies bc ON er.BaseCurrencyId = bc.Id " +
                          "JOIN Currencies tc ON er.TargetCurrencyId = tc.Id " +
                          "WHERE  er.Id = @Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        var exchangeRate = await db.QuerySingleOrDefaultAsync(
            commandText,
            reader => new ExchangeRateEntity()
            {
                Id = reader.GetString(0),
                BaseCurrency = new CurrencyEntity
                {
                    Id = reader.GetString(1),
                    Code = reader.GetString(2),
                    FullName = reader.GetString(4),
                    Sign = reader.GetString(5),
                },
                TargetCurrency = new CurrencyEntity
                {
                    Id = reader.GetString(6),
                    Code = reader.GetString(7),
                    FullName = reader.GetString(8),
                    Sign = reader.GetString(9),
                },
                Rate = reader.GetDecimal(10)
            },
            parameters
        );

        var isReceived = exchangeRate != null;

        return isReceived
            ? new BaseResult<ExchangeRateEntity>(
                operationStatus: OperationStatus.Received,
                data: exchangeRate!)
            : new BaseResult<ExchangeRateEntity>(
                operationStatus: OperationStatus.Failed);
    }

    public async Task<IBaseResult<IEnumerable<ExchangeRateEntity>>> GetAll(int limit, int offset)
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

        var exchangeRates = await db.QueryAsync(
            commandText,
            reader => new ExchangeRateEntity()
            {
                Id = reader.GetString(0),
                BaseCurrency = new CurrencyEntity
                {
                    Id = reader.GetString(1),
                    Code = reader.GetString(2),
                    FullName = reader.GetString(4),
                    Sign = reader.GetString(5),
                },
                TargetCurrency = new CurrencyEntity
                {
                    Id = reader.GetString(6),
                    Code = reader.GetString(7),
                    FullName = reader.GetString(8),
                    Sign = reader.GetString(9),
                },
                Rate = reader.GetDecimal(10)
            },
            parameters
        );

        var exchangeRatesList = exchangeRates.ToList();

        var isReceived = exchangeRatesList.Count > 0;

        return isReceived
            ? new BaseResult<IEnumerable<ExchangeRateEntity>>(
                operationStatus: OperationStatus.Received,
                data: exchangeRatesList!)
            : new BaseResult<IEnumerable<ExchangeRateEntity>>(
                operationStatus: OperationStatus.Failed);
    }

    public async Task<IBaseResult<Guid>> Delete(Guid id)
    {
        var commandText = "DELETE FROM ExchangeRates " +
                          "WHERE Id=@Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        var affectedRows = await db.ExecuteAsync(commandText, parameters);

        var isDeleted = affectedRows > 0;

        return isDeleted
            ? new BaseResult<Guid>(
                operationStatus: OperationStatus.Deleted,
                data: id)
            : new BaseResult<Guid>(
                operationStatus: OperationStatus.Failed);
    }

    public async Task<IBaseResult<ExchangeRate>> Update(ExchangeRate exchangeRate)
    {
        var commandText = "UPDATE ExchangeRates " +
                          "SET BaseCurrencyId = @BaseCurrencyId, " +
                          "TargetCurrencyId = @TargetCurrencyId, " +
                          "Rate = @Rate " +
                          "WHERE Id = @OldId;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", exchangeRate.Id),
            new SqliteParameter("@BaseCurrencyId", exchangeRate.BaseCurrency.Id),
            new SqliteParameter("@TargetCurrencyId", exchangeRate.TargetCurrency.Id),
            new SqliteParameter("@Rate", exchangeRate.Rate)
        };

        var affectedRows = await db.ExecuteAsync(commandText, parameters);

        var isUpdated = affectedRows > 0;

        return isUpdated
            ? new BaseResult<ExchangeRate>(
                operationStatus: OperationStatus.Updated,
                data: exchangeRate)
            : new BaseResult<ExchangeRate>(
                operationStatus: OperationStatus.Failed);
    }
}