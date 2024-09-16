using CurrencyExchange.DAL.Commons;
using CurrencyExchange.DAL.Entities;
using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Enums;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Result;
using Microsoft.Data.Sqlite;

namespace CurrencyExchange.DAL.Repository.Implementations;

public class CurrencyRepository(DataBase db)
    : ICurrencyRepository
{
    public async Task<IBaseResult<Currency>> Create(Currency currency)
    {
        var commandText = "INSERT INTO Currencies (Id, Code, FullName, Sign) " +
                          "VALUES (@Id, @Code, @FullName, @Sign);";

        var parameters = new[]
        {
            new SqliteParameter("@Id", currency.Id),
            new SqliteParameter("@Code", currency.Code),
            new SqliteParameter("@FullName", currency.FullName),
            new SqliteParameter("@Sign", currency.Sign)
        };

        var affectedRows = await db.ExecuteAsync(commandText, parameters);

        var isCreated = affectedRows > 0;

        return isCreated
            ? new BaseResult<Currency>(
                operationStatus: OperationStatus.Created,
                data: currency)
            : new BaseResult<Currency>(
                operationStatus: OperationStatus.Failed);
    }

    public async Task<IBaseResult<CurrencyEntity>> GetById(Guid id)
    {
        var commandText = "SELECT * FROM Currencies WHERE Id = @Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        var currencyEntity = await db.QuerySingleOrDefaultAsync(
            commandText,
            reader => new CurrencyEntity()
            {
                Id = reader.GetString(0),
                Code = reader.GetString(1),
                FullName = reader.GetString(2),
                Sign = reader.GetString(3)
            },
            parameters
        );

        var isReceived = currencyEntity != null;

        return isReceived
            ? new BaseResult<CurrencyEntity>(
                operationStatus: OperationStatus.Received,
                data: currencyEntity!)
            : new BaseResult<CurrencyEntity>(
                operationStatus: OperationStatus.Failed);
    }

    public async Task<IBaseResult<IEnumerable<CurrencyEntity>>> GetAll
        (int limit, int offset)
    {
        var commandText = "SELECT * FROM Currencies";
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

        var currencyEntities = await db.QueryAsync(
            commandText,
            reader => new CurrencyEntity()
            {
                Id = reader.GetString(0),
                Code = reader.GetString(1),
                FullName = reader.GetString(2),
                Sign = reader.GetString(3)
            },
            parameters
        );

        var currenciesList = currencyEntities.ToList();

        var isReceived = currenciesList.Count > 0;

        return isReceived
            ? new BaseResult<IEnumerable<CurrencyEntity>>(
                operationStatus: OperationStatus.Received,
                data: currenciesList)
            : new BaseResult<IEnumerable<CurrencyEntity>>(
                operationStatus: OperationStatus.Failed);
    }

    public async Task<IBaseResult<Guid>> Delete(Guid id)
    {
        var commandText = "DELETE FROM Currencies " +
                          "WHERE Id=@Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        var affectedRows = await db.ExecuteAsync(commandText, parameters);

        var isDeleted = affectedRows > 0;

        return isDeleted
            ? new BaseResult<Guid>(
                operationStatus: OperationStatus.Received,
                data: id)
            : new BaseResult<Guid>(
                operationStatus: OperationStatus.Failed);
    }

    public async Task<IBaseResult<Currency>> Update(Currency currency)
    {
        var commandText = "UPDATE Currencies " +
                          "SET Code = @Code, " +
                          "FullName = @FullName, " +
                          "Sign = @Sign " +
                          "WHERE Id = @OldId;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", currency.Id),
            new SqliteParameter("@Code", currency.Code),
            new SqliteParameter("@FullName", currency.FullName),
            new SqliteParameter("@Sign", currency.Sign)
        };

        var affectedRows = await db.ExecuteAsync(commandText, parameters);

        var isUpdated = affectedRows > 0;

        return isUpdated
            ? new BaseResult<Currency>(
                operationStatus: OperationStatus.Updated,
                data: currency)
            : new BaseResult<Currency>(
                operationStatus: OperationStatus.Failed);
    }
}