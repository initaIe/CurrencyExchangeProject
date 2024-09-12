using CurrencyExchange.DAL.Commons;
using CurrencyExchange.DAL.DAO.DTOs.Currency;
using CurrencyExchange.DAL.DAO.Interfaces;
using Microsoft.Data.Sqlite;

namespace CurrencyExchange.DAL.DAO.Implementations;

public class CurrencyDAOImpl(DataBaseHelper dbHelper)
    : IBaseDAO<CurrencyDAO>
{
    public async Task<bool> CreateAsync(CurrencyDAO entity)
    {
        var commandText = "INSERT INTO Currencies (Id, Code, FullName, Sign) " +
                          "VALUES (@Id, @Code, @FullName, @Sign);";

        var parameters = new[]
        {
            new SqliteParameter("@Id", entity.Id),
            new SqliteParameter("@Code", entity.Code),
            new SqliteParameter("@FullName", entity.FullName),
            new SqliteParameter("@Sign", entity.Sign)
        };

        var affectedRows = await dbHelper.ExecuteAsync(commandText, parameters);

        return affectedRows > 0;
    }

    public async Task<CurrencyDAO?> GetByIdAsync(Guid id)
    {
        var commandText = "SELECT * FROM Currencies WHERE Id = @Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        return await dbHelper.QuerySingleOrDefaultAsync(
            commandText,
            reader => new CurrencyDAO
            {
                Id = reader.GetGuid(0),
                Code = reader.GetString(1),
                FullName = reader.GetString(2),
                Sign = reader.GetString(3)
            },
            parameters
        );
    }

    public async Task<IEnumerable<CurrencyDAO>> GetAllAsync()
    {
        var commandText = "SELECT * FROM Currencies;";

        return await dbHelper.QueryAsync(
            commandText,
            reader => new CurrencyDAO
            {
                Id = reader.GetGuid(0),
                Code = reader.GetString(1),
                FullName = reader.GetString(2),
                Sign = reader.GetString(3)
            }
        );
    }

    public async Task<IEnumerable<CurrencyDAO>> GetAllAsync(int pageSize, int pageNumber)
    {
        var offset = (pageNumber - 1) * pageSize;

        var commandText = "SELECT * FROM Currencies LIMIT @Limit OFFSET @Offset;";

        var parameters = new[]
        {
            new SqliteParameter("@Limit", pageSize),
            new SqliteParameter("@Offset", offset)
        };

        return await dbHelper.QueryAsync(
            commandText,
            reader => new CurrencyDAO
            {
                Id = reader.GetGuid(0),
                Code = reader.GetString(1),
                FullName = reader.GetString(2),
                Sign = reader.GetString(3)
            },
            parameters
        );
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var commandText = "DELETE FROM Currencies " +
                          "WHERE Id=@Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        var affectedRows = await dbHelper.ExecuteAsync(commandText, parameters);

        return affectedRows > 0;
    }

    public async Task<bool> UpdateAsync(CurrencyDAO entity)
    {
        var commandText = "UPDATE Currencies " +
                          "SET Code = @Code, " +
                          "FullName = @FullName, " +
                          "Sign = @Sign " +
                          "WHERE Id = @Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", entity.Id),
            new SqliteParameter("@Code", entity.Code),
            new SqliteParameter("@FullName", entity.FullName),
            new SqliteParameter("@Sign", entity.Sign)
        };

        var affectedRows = await dbHelper.ExecuteAsync(commandText, parameters);

        return affectedRows > 0;
    }
}