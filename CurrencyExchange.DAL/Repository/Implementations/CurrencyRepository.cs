using CurrencyExchange.DAL.Commons;
using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Result.Implementations;
using CurrencyExchange.Domain.Result.Interfaces;
using Microsoft.Data.Sqlite;

namespace CurrencyExchange.DAL.Repository.Implementations;

public class CurrencyRepository(DataBase db)
    : ICurrencyRepository
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
            : Result<Currency>.Failure("[DB] Create currency error: conflict.");
    }

    public async Task<IResult<Currency>> GetByIdAsync(Guid id)
    {
        var commandText = "SELECT * FROM Currencies WHERE Id = @Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        var currencyEntity = await db.QuerySingleOrDefaultAsync(
            commandText,
            reader => Currency.CreateWithoutValidation(
                Guid.Parse(reader.GetString(0)),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3)),
            parameters
        );

        var isReceived = currencyEntity != null;

        return isReceived
            ? Result<Currency>.Success(currencyEntity!)
            : Result<Currency>.Failure("[DB] GetById currency error: not found.");
    }

    public async Task<IResult<Currency>> GetByCodeAsync(string code)
    {
        var commandText = "SELECT * FROM Currencies WHERE Code = @Code;";

        var parameters = new[]
        {
            new SqliteParameter("@Code", code)
        };

        var currencyEntity = await db.QuerySingleOrDefaultAsync(
            commandText,
            reader => Currency.CreateWithoutValidation
            (
                Guid.Parse(reader.GetString(0)),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3)),
            parameters
        );

        var isReceived = currencyEntity != null;

        return isReceived
            ? Result<Currency>.Success(currencyEntity!)
            : Result<Currency>.Failure("[DB] GetByCode error: not found.");
    }

    public async Task<IResult<IEnumerable<Currency>>> GetAllAsync
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
            reader => Currency.CreateWithoutValidation
            (
                Guid.Parse(reader.GetString(0)),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3)
            ),
            parameters
        );

        var currenciesList = currencyEntities.ToList();

        var isReceived = currenciesList.Count > 0;

        return isReceived
            ? Result<IEnumerable<Currency>>.Success(currenciesList)
            : Result<IEnumerable<Currency>>.Failure("[DB] GetAll currencies error: not found.");
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
            : Result<Guid>.Failure("[DB] Delete currency error: not found.");
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
            new SqliteParameter("@Id", id),
            new SqliteParameter("@Code", currency.Code),
            new SqliteParameter("@FullName", currency.FullName),
            new SqliteParameter("@Sign", currency.Sign)
        };

        var affectedRows = await db.ExecuteAsync(commandText, parameters);

        var isUpdated = affectedRows > 0;

        return isUpdated
            ? Result<Currency>.Success(currency)
            : Result<Currency>.Failure("[DB] Update currency error: conflict/not found.");
    }
}