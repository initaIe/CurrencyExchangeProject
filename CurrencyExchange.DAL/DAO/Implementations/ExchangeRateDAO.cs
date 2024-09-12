using CurrencyExchange.DAL.Commons;
using CurrencyExchange.DAL.DAO.Interfaces;
using CurrencyExchange.DAL.DTOs;
using CurrencyExchange.DAL.Result;
using Microsoft.Data.Sqlite;

namespace CurrencyExchange.DAL.DAO.Implementations;

public class ExchangeRateDAO(DataBaseHelper dbHelper)
    : IBaseDAO<ExchangeRateDTO>
{
    public async Task<IBaseResult<ExchangeRateDTO>> Create(CreateExchangeRateDTO dto)
    {
        var commandText = "INSERT INTO ExchangeRates (Id, BaseCurrencyId, TargetCurrencyId, Rate) " +
                          "VALUES (@Id, @BaseCurrencyId, @TargetCurrencyId, @Rate);";

        var parameters = new[]
        {
            new SqliteParameter("@Id", dto.Id),
            new SqliteParameter("@BaseCurrencyId", dto.BaseCurrencyId),
            new SqliteParameter("@TargetCurrencyId", dto.TargetCurrencyId),
            new SqliteParameter("@Rate", dto.Rate)
        };

        var affectedRows = await dbHelper.ExecuteAsync(commandText, parameters);

        var isCreated = affectedRows > 0;

        return new BaseResult<ExchangeRateDTO>
        {
            IsSuccess = isCreated,
            Message = isCreated
                ? $"ExchangeRate {dto.Id} has been created"
                : $"ExchangeRate {dto.Id} has not been created"
        };
    }

    public async Task<IBaseResult<ExchangeRateDTO>> GetById(Guid id)
    {
        var commandText = "SELECT * FROM ExchangeRates WHERE Id = @Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        var exchangeRate = await dbHelper.QuerySingleOrDefaultAsync(
            commandText,
            reader => new ExchangeRateDTO
            {
                Id = reader.GetInt32(0),
                BaseCurrencyId = reader.GetInt32(1),
                TargetCurrencyId = reader.GetInt32(2),
                Rate = reader.GetDecimal(3)
            },
            parameters
        );

        var isReceived = exchangeRate != null;

        return new BaseResult<ExchangeRateDTO>
        {
            IsSuccess = isReceived,
            Message = isReceived
                ? $"ExchangeRate with Id {id} was received"
                : $"ExchangeRate with Id {id} was not received",
            Data = isReceived
                ? exchangeRate
                : null
        };
    }

    public async Task<IBaseResult<IEnumerable<ExchangeRateDTO>>> GetAll(int entitiesLimit, int entitiesOffset)
    {
        var offset = (entitiesOffset - 1) * entitiesLimit;

        var commandText = "SELECT * FROM ExchangeRates LIMIT @Limit OFFSET @Offset;";

        var parameters = new[]
        {
            new SqliteParameter("@Limit", entitiesLimit),
            new SqliteParameter("@Offset", offset)
        };

        var exchangeRates =  await dbHelper.QueryAsync(
            commandText,
            reader => new ExchangeRateDTO
            {
                Id = reader.GetInt32(0),
                BaseCurrencyId = reader.GetInt32(1),
                TargetCurrencyId = reader.GetInt32(2),
                Rate = reader.GetDecimal(3)
            },
            parameters
        );
        
        var exchangeRatesList = exchangeRates.ToList();
        var isReceived = exchangeRatesList.Count > 0;

        return new BaseResult<IEnumerable<ExchangeRateDTO>>
        {
            IsSuccess = isReceived,
            Message = isReceived
                ? $"ExchangeRate was received"
                : $"ExchangeRate was not received",
            Data = isReceived
                ? exchangeRatesList
                : null
        };
    }

    public async Task<IBaseResult<ExchangeRateDTO>> Delete(Guid id)
    {
        var commandText = "DELETE FROM ExchangeRates " +
                          "WHERE Id=@Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        var affectedRows = await dbHelper.ExecuteAsync(commandText, parameters);

        var isDeleted = affectedRows > 0;

        return new BaseResult<ExchangeRateDTO>
        {
            IsSuccess = isDeleted,
            Message = isDeleted
                ? $"ExchangeRate with Id {id} has been deleted"
                : $"ExchangeRate with Id {id} has not been deleted"
        };
    }

    public async Task<IBaseResult<ExchangeRateDTO>> Update(Guid id, ExchangeRateDTO dto)
    {
        var commandText = "UPDATE ExchangeRates " +
                          "SET Id = @NewId, " +
                          "BaseCurrencyId = @BaseCurrencyId, " +
                          "TargetCurrencyId = @TargetCurrencyId, " +
                          "Rate = @Rate " +
                          "WHERE Id = @OldId;";

        var parameters = new[]
        {
            new SqliteParameter("@OldId", id),
            new SqliteParameter("@NewId", dto.Id),
            new SqliteParameter("@BaseCurrencyId", dto.BaseCurrencyId),
            new SqliteParameter("@TargetCurrencyId", dto.TargetCurrencyId),
            new SqliteParameter("@Rate", dto.Rate)
        };

        var affectedRows = await dbHelper.ExecuteAsync(commandText, parameters);

        var isUpdated = affectedRows > 0;

        return new BaseResult<ExchangeRateDTO>
        {
            IsSuccess = isUpdated,
            Message = isUpdated
                ? $"ExchangeRate with new Id {dto.Id} has been created"
                : $"ExchangeRate with old Id {id} has not been created"
        };
    }
}