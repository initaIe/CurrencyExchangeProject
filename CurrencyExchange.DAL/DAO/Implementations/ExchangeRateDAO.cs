using CurrencyExchange.DAL.Commons;
using CurrencyExchange.DAL.DAO.Interfaces;
using CurrencyExchange.DTOs.ExchangeRate;
using Microsoft.Data.Sqlite;

namespace CurrencyExchange.DAL.DAO.Implementations;

public class ExchangeRateDAO(DataBaseHelper dbHelper)
    : IBaseDAO<CreateExchangeRateDTO, GetExchangeRateDTO, UpdateExchangeRateDTO>
{
    public async Task<bool> CreateAsync(CreateExchangeRateDTO entity)
    {
        var commandText = "INSERT INTO ExchangeRates (BaseCurrencyId, TargetCurrencyId, Rate) " +
                          "VALUES (@BaseCurrencyId, @TargetCurrencyId, @Rate);";

        var parameters = new[]
        {
            new SqliteParameter("@BaseCurrencyId", entity.BaseCurrencyId),
            new SqliteParameter("@TargetCurrencyId", entity.TargetCurrencyId),
            new SqliteParameter("@Rate", entity.Rate)
        };

        var affectedRows = await dbHelper.ExecuteAsync(commandText, parameters);

        return affectedRows > 0;
    }

    public async Task<GetExchangeRateDTO> GetByIdAsync(int id)
    {
        var commandText = "SELECT * FROM ExchangeRates WHERE Id = @Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        return await dbHelper.QuerySingleAsync(
            commandText,
            reader => new GetExchangeRateDTO
            {
                Id = reader.GetInt32(0),
                BaseCurrencyId = reader.GetInt32(1),
                TargetCurrencyId = reader.GetInt32(2),
                Rate = reader.GetDecimal(3)
            },
            parameters
        );
    }

    public async Task<IEnumerable<GetExchangeRateDTO>> GetAllAsync()
    {
        var commandText = "SELECT * FROM ExchangeRates;";

        return await dbHelper.QueryAsync(
            commandText,
            reader => new GetExchangeRateDTO
            {
                Id = reader.GetInt32(0),
                BaseCurrencyId = reader.GetInt32(1),
                TargetCurrencyId = reader.GetInt32(2),
                Rate = reader.GetDecimal(3)
            }
        );
    }

    public async Task<IEnumerable<GetExchangeRateDTO>> GetAllAsync(int pageSize, int pageNumber)
    {
        var offset = (pageNumber - 1) * pageSize;

        var commandText = "SELECT * FROM ExchangeRates LIMIT @Limit OFFSET @Offset;";

        var parameters = new[]
        {
            new SqliteParameter("@Limit", pageSize),
            new SqliteParameter("@Offset", offset)
        };

        return await dbHelper.QueryAsync(
            commandText,
            reader => new GetExchangeRateDTO
            {
                Id = reader.GetInt32(0),
                BaseCurrencyId = reader.GetInt32(1),
                TargetCurrencyId = reader.GetInt32(2),
                Rate = reader.GetDecimal(3)
            },
            parameters
        );
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var commandText = "DELETE FROM ExchangeRates " +
                          "WHERE Id=@Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        var affectedRows = await dbHelper.ExecuteAsync(commandText, parameters);

        return affectedRows > 0;
    }

    public async Task<bool> UpdateAsync(UpdateExchangeRateDTO entity)
    {
        var commandText = "UPDATE ExchangeRates " +
                          "SET BaseCurrencyId = @BaseCurrencyId, " +
                          "TargetCurrencyId = @TargetCurrencyId, " +
                          "Rate = @Rate " +
                          "WHERE Id = @Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", entity.Id),
            new SqliteParameter("@BaseCurrencyId", entity.BaseCurrencyId),
            new SqliteParameter("@TargetCurrencyId", entity.TargetCurrencyId),
            new SqliteParameter("@Rate", entity.Rate)
        };

        var affectedRows = await dbHelper.ExecuteAsync(commandText, parameters);

        return affectedRows > 0;
    }
}