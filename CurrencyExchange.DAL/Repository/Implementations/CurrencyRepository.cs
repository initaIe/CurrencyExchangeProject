using CurrencyExchange.DAL.Commons;
using CurrencyExchange.DAL.Entities;
using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Result;
using CurrencyExchange.Domain.Result.Implementations;
using CurrencyExchange.Domain.Result.Interfaces;
using Microsoft.Data.Sqlite;

namespace CurrencyExchange.DAL.Repository.Implementations;

public class CurrencyRepository(DataBase db)
    : IRepository<Currency, CurrencyEntity>
{
    public async Task<IResult<Currency>> CreateAsync(Currency currency)
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
            ? Result<Currency>.Success(currency)
            : Result<Currency>.Failure();
    }

    public async Task<IResult<CurrencyEntity>> GetByIdAsync(Guid id)
    {
        var commandText = "SELECT * FROM Currencies WHERE Id = @Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        var currencyEntity = await db.QuerySingleOrDefaultAsync(
            commandText,
            reader => new CurrencyEntity
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
            ? Result<CurrencyEntity>.Success(currencyEntity!)
            : Result<CurrencyEntity>.Failure();
    }

    public async Task<IResult<IEnumerable<CurrencyEntity>>> GetAllAsync
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
            reader => new CurrencyEntity
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
            ? Result<IEnumerable<CurrencyEntity>>.Success(currenciesList)
            : Result<IEnumerable<CurrencyEntity>>.Failure();
    }

    public async Task<IResult<Guid>> DeleteAsync(Guid id)
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
            ? Result<Guid>.Success(id)
            : Result<Guid>.Failure();
    }

    public async Task<IResult<Currency>> UpdateAsync(Guid id, Currency currency)
    {
        var commandText = "UPDATE Currencies " +
                          "SET Code = @Code, " +
                          "FullName = @FullName, " +
                          "Sign = @Sign " +
                          "WHERE Id = @Id;";

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
            ? Result<Currency>.Success(currency)
            : Result<Currency>.Failure();
    }
}