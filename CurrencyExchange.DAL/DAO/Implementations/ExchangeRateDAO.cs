using CurrencyExchange.DAL.DAO.Interfaces;
using CurrencyExchange.DTOs.ExchangeRate;
using Microsoft.Data.Sqlite;

namespace CurrencyExchange.DAL.DAO.Implementations;

public class ExchangeRateDAO(SqliteRepository repository)
    : IBaseDAO<CreateExchangeRateDTO, GetExchangeRateDTO, UpdateExchangeRateDTO>
{
    public async Task<bool> Create(CreateExchangeRateDTO entity)
    {
        var commandText = "INSERT INTO ExchangeRates (BaseCurrencyId, TargetCurrencyId, Rate) " +
                          "VALUES (@BaseCurrencyId, @TargetCurrencyId, @Rate);";

        var parameters = new[]
        {
            new SqliteParameter("@BaseCurrencyId", entity.BaseCurrencyId),
            new SqliteParameter("@TargetCurrencyId", entity.TargetCurrencyId),
            new SqliteParameter("@Rate", entity.Rate)
        };

        var affectedRows = await repository.ExecuteAsync(commandText, parameters);

        return affectedRows > 0;
    }

    public async Task<GetExchangeRateDTO> GetById(int id)
    {
        var commandText = "SELECT * FROM ExchangeRates WHERE Id = @Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        return await repository.QuerySingleAsync(
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

    public async Task<IEnumerable<GetExchangeRateDTO>> GetAll()
    {
        var commandText = "SELECT * FROM ExchangeRates;";

        return await repository.QueryAsync(
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

    public async Task<bool> Delete(int id)
    {
        var commandText = "DELETE FROM ExchangeRates " +
                          "WHERE Id=@Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        var affectedRows = await repository.ExecuteAsync(commandText, parameters);

        return affectedRows > 0;
    }

    public async Task<bool> Update(UpdateExchangeRateDTO entity)
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

        var affectedRows = await repository.ExecuteAsync(commandText, parameters);

        return affectedRows > 0;
    }
}