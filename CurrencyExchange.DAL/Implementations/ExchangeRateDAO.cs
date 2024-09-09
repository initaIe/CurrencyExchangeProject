using CurrencyExchange.DAL.Interfaces;
using CurrencyExchange.Domain.Entity;
using Microsoft.Data.Sqlite;

namespace CurrencyExchange.DAL.Implementations;

public class ExchangeRateDAO(SqliteRepository repository) : IBaseDAO<ExchangeRate>
{

    public async Task Create(ExchangeRate entity)
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

    public async Task<ExchangeRate> Read(int id)
    {
        var commandText = "SELECT * FROM ExchangeRates WHERE Id = @Id";
    
        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };
    
        return await repository.QuerySingleAsync(
            commandText,
            reader => new ExchangeRate
            {
                Id = reader.GetInt32(0),
                BaseCurrencyId = reader.GetInt32(1),
                TargetCurrencyId = reader.GetInt32(2),
                Rate = reader.GetDecimal(3)
            },
            parameters
        );
    }

    public Task<IEnumerable<ExchangeRate>> ReadAll()
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(ExchangeRate entity)
    {
        throw new NotImplementedException();
    }
}