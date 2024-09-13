using CurrencyExchange.DAL.Commons;
using CurrencyExchange.DAL.DTOs;
using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.DAL.Result;
using CurrencyExchange.Domain.Entities;
using Microsoft.Data.Sqlite;

namespace CurrencyExchange.DAL.Repository.Implementations;

public class CurrencyRepository(DataBase db)
    : IBaseRepository<Currency, CurrencyDTO>
{
    public async Task<IBaseResult<Currency>> Create(CurrencyDTO dto)
    {
        var commandText = "INSERT INTO Currencies (Id, Code, FullName, Sign) " +
                          "VALUES (@Id, @Code, @FullName, @Sign);";

        var parameters = new[]
        {
            new SqliteParameter("@Id", dto.Id),
            new SqliteParameter("@Code", dto.Code),
            new SqliteParameter("@FullName", dto.FullName),
            new SqliteParameter("@Sign", dto.Sign)
        };

        var affectedRows = await db.ExecuteAsync(commandText, parameters);

        var isCreated = affectedRows > 0;

        return new BaseResult<Currency>
        {
            IsSuccess = isCreated,
            Message = isCreated
                ? $"Currency with Id {dto.Id} has been created"
                : $"Currency with Id {dto.Id} has not been created"
        };
    }

    public async Task<IBaseResult<Currency>> GetById(Guid id)
    {
        var commandText = "SELECT * FROM Currencies WHERE Id = @Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        var currencyCreationResult = await db.QuerySingleOrDefaultAsync(
            commandText,
            reader => Currency.Create(
                reader.GetGuid(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3)),
            parameters
        );

        var isReceived = currencyCreationResult.currency != null;

        return new BaseResult<Currency>
        {
            IsSuccess = isReceived,
            Message = isReceived
                ? $"Currency with Id {id} was received"
                : $"Currency with Id {id} was not received. Currency creation error: [{currencyCreationResult.error}]",
            Data = isReceived
                ? currencyCreationResult.currency
                : null
        };
    }

    public async Task<IBaseResult<IEnumerable<Currency>>> GetAll
        (int limit = 0, int offset = 0)
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

        var currenciesCreationResult = await db.QueryAsync(
            commandText,
            reader => Currency.Create(
                reader.GetGuid(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3)),
            parameters
        );

        var currenciesList = currenciesCreationResult.ToList();

        var isReceived = currenciesList.Count > 0;

        var errors = currenciesList
            .Where(c => !string.IsNullOrEmpty(c.error))
            .Select(c => c.error)
            .ToList();

        var validCurrencies = currenciesList
            .Where(c => c.currency != null)
            .Select(c => c.currency!)
            .ToList();

        var errorMessage = errors.Count > 0
            ? $"Some currencies were not created due to errors: {string.Join("; ", errors)}"
            : null;

        return new BaseResult<IEnumerable<Currency>>
        {
            IsSuccess = isReceived && errors.Count == 0,
            Message = isReceived
                ? errorMessage
                  ?? "All currencies were successfully received"
                : "Not all currencies were received",
            Data = validCurrencies.Count != 0 ? validCurrencies : null
        };
    }

    public async Task<IBaseResult<Currency>> Delete(Guid id)
    {
        var commandText = "DELETE FROM Currencies " +
                          "WHERE Id=@Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        var affectedRows = await db.ExecuteAsync(commandText, parameters);

        var isDeleted = affectedRows > 0;

        return new BaseResult<Currency>
        {
            IsSuccess = isDeleted,
            Message = isDeleted
                ? $"Currency with Id {id} has been deleted"
                : $"Currency with Id {id} has not been deleted"
        };
    }

    public async Task<IBaseResult<Currency>> Update(Guid id, CurrencyDTO dto)
    {
        var commandText = "UPDATE Currencies " +
                          "SET Id = @NewId, " +
                          "Code = @Code, " +
                          "FullName = @FullName, " +
                          "Sign = @Sign " +
                          "WHERE Id = @OldId;";

        var parameters = new[]
        {
            new SqliteParameter("@OldId", id),
            new SqliteParameter("@NewId", dto.Id),
            new SqliteParameter("@Code", dto.Code),
            new SqliteParameter("@FullName", dto.FullName),
            new SqliteParameter("@Sign", dto.Sign)
        };

        var affectedRows = await db.ExecuteAsync(commandText, parameters);

        var isUpdated = affectedRows > 0;

        return new BaseResult<Currency>
        {
            IsSuccess = isUpdated,
            Message = isUpdated
                ? $"Currency with Id {dto.Id} has been updated"
                : $"Currency with Id {id} has not been updated"
        };
    }
}