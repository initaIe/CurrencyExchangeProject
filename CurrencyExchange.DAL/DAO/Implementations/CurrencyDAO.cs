using CurrencyExchange.DAL.DAO.Interfaces;
using CurrencyExchange.Domain.Entity;
using CurrencyExchange.DTOs.Currency;
using CurrencyExchange.DTOs.ExchangeRate;
using Microsoft.Data.Sqlite;

namespace CurrencyExchange.DAL.DAO.Implementations;

public class CurrencyDAO(SqliteRepository repository)
    : IBaseDAO<CreateCurrencyDTO, GetCurrencyDTO, UpdateCurrencyDTO>
{
    public async Task<bool> Create(CreateCurrencyDTO entity)
    {
        var commandText = "INSERT INTO Currencies (Code, FullName, Sign) " +
                          "VALUES (@Code, @FullName, @Sign);";

        var parameters = new[]
        {
            new SqliteParameter("@Code", entity.Code),
            new SqliteParameter("@FullName", entity.FullName),
            new SqliteParameter("@Sign", entity.Sign)
        };

        var affectedRows = await repository.ExecuteAsync(commandText, parameters);

        return affectedRows > 0;
    }

    public async Task<GetCurrencyDTO> GetById(int id)
    {
        var commandText = "SELECT * FROM Currencies WHERE Id = @Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        return await repository.QuerySingleAsync(
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

    public async Task<IEnumerable<GetCurrencyDTO>> GetAll()
    {
        var commandText = "SELECT * FROM Currencies;";

        return await repository.QueryAsync(
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

    public async Task<bool> Delete(int id)
    {
        var commandText = "DELETE FROM Currencies " +
                          "WHERE Id=@Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        var affectedRows = await repository.ExecuteAsync(commandText, parameters);

        return affectedRows > 0;
    }

    public async Task<bool> Update(UpdateCurrencyDTO entity)
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

        var affectedRows = await repository.ExecuteAsync(commandText, parameters);

        return affectedRows > 0;
    }
}