using CurrencyExchange.DAL.Commons;
using CurrencyExchange.DAL.DAO.Interfaces;
using CurrencyExchange.DTOs.Currency;
using Microsoft.Data.Sqlite;

namespace CurrencyExchange.DAL.DAO.Implementations;

public class CurrencyDAO(DataBaseHelper dbHelper)
    : IBaseDAO<CreateCurrencyDTO, GetCurrencyDTO, UpdateCurrencyDTO>
{
    public async Task<bool> CreateAsync(CreateCurrencyDTO entity)
    {
        var commandText = "INSERT INTO Currencies (Code, FullName, Sign) " +
                          "VALUES (@Code, @FullName, @Sign);";

        var parameters = new[]
        {
            new SqliteParameter("@Code", entity.Code),
            new SqliteParameter("@FullName", entity.FullName),
            new SqliteParameter("@Sign", entity.Sign)
        };

        var affectedRows = await dbHelper.ExecuteAsync(commandText, parameters);

        return affectedRows > 0;
    }

    public async Task<GetCurrencyDTO?> GetByIdAsync(int id)
    {
        var commandText = "SELECT * FROM Currencies WHERE Id = @Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        return await dbHelper.QuerySingleOrDefaultAsync(
            commandText,
            reader => new GetCurrencyDTO
            {
                Id = reader.GetInt32(0),
                Code = reader.GetString(1),
                FullName = reader.GetString(2),
                Sign = reader.GetString(3)
            },
            parameters
        );
    }

    public async Task<IEnumerable<GetCurrencyDTO>> GetAllAsync()
    {
        var commandText = "SELECT * FROM Currencies;";

        return await dbHelper.QueryAsync(
            commandText,
            reader => new GetCurrencyDTO
            {
                Id = reader.GetInt32(0),
                Code = reader.GetString(1),
                FullName = reader.GetString(2),
                Sign = reader.GetString(3)
            }
        );
    }

    public async Task<IEnumerable<GetCurrencyDTO>> GetAllAsync(int pageSize, int pageNumber)
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
            reader => new GetCurrencyDTO
            {
                Id = reader.GetInt32(0),
                Code = reader.GetString(1),
                FullName = reader.GetString(2),
                Sign = reader.GetString(3)
            },
            parameters
        );
    }

    public async Task<bool> DeleteAsync(int id)
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

    public async Task<bool> UpdateAsync(UpdateCurrencyDTO entity)
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