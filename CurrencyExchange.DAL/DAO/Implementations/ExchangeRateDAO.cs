using CurrencyExchange.DAL.Interfaces;
using CurrencyExchange.DTOs.ExchangeRates;
using Microsoft.Data.Sqlite;

namespace CurrencyExchange.DAL.Implementations;

public class ExchangeRateDAO(SqliteRepository repository)
    : IBaseDAO<CreateExchangeRate, ReadExchangeRate, UpdateExchangeRate>
{
    public async Task Create(CreateExchangeRate entity)
    {
        var commandText = "INSERT INTO ExchangeRates (BaseCurrencyId, TargetCurrencyId, Rate) " +
                          "VALUES (@BaseCurrencyId, @TargetCurrencyId, @Rate)";

        var parameters = new[]
        {
            new SqliteParameter("@BaseCurrencyId", entity.BaseCurrencyId),
            new SqliteParameter("@TargetCurrencyId", entity.TargetCurrencyId),
            new SqliteParameter("@Rate", entity.Rate)
        };

        await repository.ExecuteAsync(commandText, parameters);
    }

    public async Task<ReadExchangeRate> Read(int id)
    {
        var commandText = "SELECT * FROM ReadExchangeRateResponse WHERE Id = @Id";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        return await repository.QuerySingleAsync(
            commandText,
            reader => new ReadExchangeRate
            {
                Id = reader.GetInt32(0),
                BaseCurrencyId = reader.GetInt32(1),
                TargetCurrencyId = reader.GetInt32(2),
                Rate = reader.GetDecimal(3)
            },
            parameters
        );
    }

    public async Task<IEnumerable<ReadExchangeRate>> ReadAll()
    {
        var commandText = "SELECT * FROM ReadExchangeRateResponse";

        return await repository.QueryAsync(
            commandText,
            reader => new ReadExchangeRate
            {
                Id = reader.GetInt32(0),
                BaseCurrencyId = reader.GetInt32(1),
                TargetCurrencyId = reader.GetInt32(2),
                Rate = reader.GetDecimal(3)
            }
        );
    }

    public async Task Delete(int id)
    {
        var commandText = "DELETE FROM ExchangeRates" +
                          "WHERE Id=@Id";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        await repository.ExecuteAsync(commandText, parameters);
    }

    public async Task Update(UpdateExchangeRate entity)
    {
        var commandText = "UPDATE ExchangeRates" +
                          "SET BaseCurrencyId = @BaseCurrencyId," +
                          "TargetCurrencyId = @TargetCurrencyId," +
                          "Rate = @Rate;";

        var parameters = new[]
        {
            new SqliteParameter("@BaseCurrencyId", entity.BaseCurrencyId),
            new SqliteParameter("@TargetCurrencyId", entity.TargetCurrencyId),
            new SqliteParameter("@Rate", entity.Rate)
        };

        await repository.ExecuteAsync(commandText, parameters);
    }
}