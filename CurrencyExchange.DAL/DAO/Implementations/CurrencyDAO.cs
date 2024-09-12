using CurrencyExchange.DAL.Commons;
using CurrencyExchange.DAL.DAO.Interfaces;
using CurrencyExchange.DAL.DTOs;
using CurrencyExchange.DAL.Result;
using Microsoft.Data.Sqlite;

namespace CurrencyExchange.DAL.DAO.Implementations;

public class CurrencyDAO(DataBaseHelper dbHelper)
    : IBaseDAO<CurrencyDTO>
{
    public async Task<IBaseResult<CurrencyDTO>> Create(CurrencyDTO dto)
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

        var affectedRows = await dbHelper.ExecuteAsync(commandText, parameters);

        var isCreated = affectedRows > 0;

        return new BaseResult<CurrencyDTO>
        {
            IsSuccess = isCreated,
            Message = isCreated
                ? $"Currency with Id {dto.Id} has been created"
                : $"Currency with Id {dto.Id} has not been created"
        };
    }

    public async Task<IBaseResult<CurrencyDTO>> GetById(Guid id)
    {
        var commandText = "SELECT * FROM Currencies WHERE Id = @Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        var currency = await dbHelper.QuerySingleOrDefaultAsync(
            commandText,
            reader => new CurrencyDTO
            {
                Id = reader.GetGuid(0),
                Code = reader.GetString(1),
                FullName = reader.GetString(2),
                Sign = reader.GetString(3)
            },
            parameters
        );

        var isReceived = currency != null;

        return new BaseResult<CurrencyDTO>
        {
            IsSuccess = isReceived,
            Message = isReceived
                ? $"Currency with Id {id} was received"
                : $"Currency with Id {id} was not received",
            Data = isReceived
                ? currency
                : null
        };
    }

    public async Task<IBaseResult<IEnumerable<CurrencyDTO>>> GetAll
        (int entitiesLimit = 0, int entitiesOffset = 0)
    {
        var commandText = "SELECT * FROM Currencies";
        var parameters = Array.Empty<SqliteParameter>();
        
        if (entitiesLimit > 0 && entitiesOffset > 0)
        {
            var offset = (entitiesOffset - 1) * entitiesLimit;
            commandText += " LIMIT @Limit OFFSET @Offset";

            parameters = new[]
            {
                new SqliteParameter("@Limit", entitiesLimit),
                new SqliteParameter("@Offset", offset)
            };
        }

        var currencies = await dbHelper.QueryAsync(
            commandText,
            reader => new CurrencyDTO
            {
                Id = reader.GetGuid(0),
                Code = reader.GetString(1),
                FullName = reader.GetString(2),
                Sign = reader.GetString(3)
            },
            parameters
        );

        var currenciesList = currencies.ToList();
        var isReceived = currenciesList.Count > 0;

        return new BaseResult<IEnumerable<CurrencyDTO>>
        {
            IsSuccess = isReceived,
            Message = isReceived
                ? $"Currencies was received"
                : $"Currencies was not received",
            Data = isReceived
                ? currenciesList
                : null
        };
    }

    public async Task<IBaseResult<CurrencyDTO>> Delete(Guid id)
    {
        var commandText = "DELETE FROM Currencies " +
                          "WHERE Id=@Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        var affectedRows = await dbHelper.ExecuteAsync(commandText, parameters);

        var isDeleted = affectedRows > 0;

        return new BaseResult<CurrencyDTO>
        {
            IsSuccess = isDeleted,
            Message = isDeleted
                ? $"Currency with Id {id} has been deleted"
                : $"Currency with Id {id} has not been deleted"
        };
    }

    public async Task<IBaseResult<CurrencyDTO>> Update(Guid id, CurrencyDTO dto)
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

        var affectedRows = await dbHelper.ExecuteAsync(commandText, parameters);

        var isUpdated = affectedRows > 0;

        return new BaseResult<CurrencyDTO>
        {
            IsSuccess = isUpdated,
            Message = isUpdated
                ? $"Currency with new Id {dto.Id} has been created"
                : $"Currency with old Id {id} has not been created"
        };
    }
}