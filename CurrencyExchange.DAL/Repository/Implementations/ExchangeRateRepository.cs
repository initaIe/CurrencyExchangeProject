using CurrencyExchange.DAL.Commons;
using CurrencyExchange.DAL.Repository.Interfaces;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Result.Implementations;
using CurrencyExchange.Domain.Result.Interfaces;
using Microsoft.Data.Sqlite;

namespace CurrencyExchange.DAL.Repository.Implementations;

public class ExchangeRateRepository(DataBase db)
    : IExchangeRateRepository
{
    public async Task<IResult<ExchangeRate>> CreateAsync(ExchangeRate exchangeRate)
    {
        var commandText = "INSERT INTO ExchangeRates (Id, BaseCurrencyId, TargetCurrencyId, Rate) " +
                          "VALUES (@Id, @BaseCurrencyId, @TargetCurrencyId, @Rate);";

        var parameters = new[]
        {
            new SqliteParameter("@Id", exchangeRate.Id),
            new SqliteParameter("@BaseCurrencyId", exchangeRate.BaseCurrency.Id),
            new SqliteParameter("@TargetCurrencyId", exchangeRate.TargetCurrency.Id),
            new SqliteParameter("@Rate", exchangeRate.Rate)
        };

        var affectedRows = await db.ExecuteAsync(commandText, parameters);

        var isCreated = affectedRows > 0;

        return isCreated
            ? Result<ExchangeRate>.Success(exchangeRate)
            : Result<ExchangeRate>.Failure("[DB] Create exchangeRate error: conflict.");
    }

    public async Task<IResult<ExchangeRate>> GetByIdAsync(Guid id)
    {
        var commandText = "SELECT " +
                          "er.Id AS ExchangeRateId, " +
                          "bc.Id AS BaseCurrencyId, " +
                          "bc.Code AS BaseCurrencyCode, " +
                          "bc.FullName AS BaseCurrencyFullName, " +
                          "bc.Sign AS BaseCurrencySign, " +
                          "tc.Id AS TargetCurrencyId, " +
                          "tc.Code AS TargetCurrencyCode, " +
                          "tc.FullName AS TargetCurrencyFullName, " +
                          "tc.Sign AS TargetCurrencySign, " +
                          "er.Rate " +
                          "FROM  ExchangeRates er " +
                          "JOIN  Currencies bc ON er.BaseCurrencyId = bc.Id " +
                          "JOIN Currencies tc ON er.TargetCurrencyId = tc.Id " +
                          "WHERE  er.Id = @Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        var exchangeRate = await db.QuerySingleOrDefaultAsync(
            commandText,
            reader => ExchangeRate.CreateWithoutValidation
            (
                Guid.Parse(reader.GetString(0)),
                Currency.CreateWithoutValidation
                (
                    Guid.Parse(reader.GetString(1)),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4)
                ),
                Currency.CreateWithoutValidation
                (
                    Guid.Parse(reader.GetString(5)),
                    reader.GetString(6),
                    reader.GetString(7),
                    reader.GetString(8)
                ),
                reader.GetDecimal(9)
            ),
            parameters
        );

        var isReceived = exchangeRate != null;

        return isReceived
            ? Result<ExchangeRate>.Success(exchangeRate!)
            : Result<ExchangeRate>.Failure("[DB] GetById exchangeRate error: not found.");
    }

    public async Task<IResult<ExchangeRate>> GetByCodePairAsync(string fromCode,
        string toCode)
    {
        var commandText = "SELECT " +
                          "er.Id AS ExchangeRateId, " +
                          "bc.Id AS BaseCurrencyId, " +
                          "bc.Code AS BaseCurrencyCode, " +
                          "bc.FullName AS BaseCurrencyFullName, " +
                          "bc.Sign AS BaseCurrencySign, " +
                          "tc.Id AS TargetCurrencyId, " +
                          "tc.Code AS TargetCurrencyCode, " +
                          "tc.FullName AS TargetCurrencyFullName, " +
                          "tc.Sign AS TargetCurrencySign, " +
                          "er.Rate " +
                          "FROM  ExchangeRates er " +
                          "JOIN  Currencies bc ON er.BaseCurrencyId = bc.Id " +
                          "JOIN Currencies tc ON er.TargetCurrencyId = tc.Id " +
                          "WHERE  bc.Code = @BaseCurrencyCode AND tc.Code = @TargetCurrencyCode;";

        var parameters = new[]
        {
            new SqliteParameter("@BaseCurrencyCode", fromCode),
            new SqliteParameter("@TargetCurrencyCode", toCode)
        };

        var exchangeRate = await db.QuerySingleOrDefaultAsync(
            commandText,
            reader => ExchangeRate.CreateWithoutValidation
            (
                Guid.Parse(reader.GetString(0)),
                Currency.CreateWithoutValidation
                (
                    Guid.Parse(reader.GetString(1)),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4)
                ),
                Currency.CreateWithoutValidation
                (
                    Guid.Parse(reader.GetString(5)),
                    reader.GetString(6),
                    reader.GetString(7),
                    reader.GetString(8)
                ),
                reader.GetDecimal(9)
            ),
            parameters
        );

        var isReceived = exchangeRate != null;

        return isReceived
            ? Result<ExchangeRate>.Success(exchangeRate!)
            : Result<ExchangeRate>.Failure("[DB] GetByCode exchangeRate error: not found.");
    }

    public async Task<IResult<IEnumerable<ExchangeRate>>> GetAllAsync(int limit, int offset)
    {
        var commandText = "SELECT " +
                          "er.Id AS ExchangeRateId, " +
                          "bc.Id AS BaseCurrencyId, " +
                          "bc.Code AS BaseCurrencyCode, " +
                          "bc.FullName AS BaseCurrencyFullName, " +
                          "bc.Sign AS BaseCurrencySign, " +
                          "tc.Id AS TargetCurrencyId, " +
                          "tc.Code AS TargetCurrencyCode, " +
                          "tc.FullName AS TargetCurrencyFullName, " +
                          "tc.Sign AS TargetCurrencySign, " +
                          "er.Rate " +
                          "FROM  ExchangeRates er " +
                          "JOIN  Currencies bc ON er.BaseCurrencyId = bc.Id " +
                          "JOIN Currencies tc ON er.TargetCurrencyId = tc.Id ";

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

        var exchangeRates = await db.QueryAsync(
            commandText,
            reader => ExchangeRate.CreateWithoutValidation
            (
                Guid.Parse(reader.GetString(0)),
                Currency.CreateWithoutValidation
                (
                    Guid.Parse(reader.GetString(1)),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4)
                ),
                Currency.CreateWithoutValidation
                (
                    Guid.Parse(reader.GetString(5)),
                    reader.GetString(6),
                    reader.GetString(7),
                    reader.GetString(8)
                ),
                reader.GetDecimal(9)
            ),
            parameters
        );

        var exchangeRatesList = exchangeRates.ToList();

        var isReceived = exchangeRatesList.Count > 0;

        return isReceived
            ? Result<IEnumerable<ExchangeRate>>.Success(exchangeRatesList)
            : Result<IEnumerable<ExchangeRate>>.Failure("[DB] GetAll exchangeRates error: not found.");

    }

    public async Task<IResult<Guid>> DeleteAsync(Guid id)
    {
        var commandText = "DELETE FROM ExchangeRates " +
                          "WHERE Id=@Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id)
        };

        var affectedRows = await db.ExecuteAsync(commandText, parameters);

        var isDeleted = affectedRows > 0;

        return isDeleted
            ? Result<Guid>.Success(id)
            : Result<Guid>.Failure("[DB] Delete exchangeRate error: not found.");
    }

    public async Task<IResult<ExchangeRate>> UpdateAsync(Guid id, ExchangeRate exchangeRate)
    {
        var commandText = "UPDATE ExchangeRates " +
                          "SET BaseCurrencyId = @BaseCurrencyId, " +
                          "TargetCurrencyId = @TargetCurrencyId, " +
                          "Rate = @Rate " +
                          "WHERE Id = @Id;";

        var parameters = new[]
        {
            new SqliteParameter("@Id", id),
            new SqliteParameter("@BaseCurrencyId", exchangeRate.BaseCurrency.Id),
            new SqliteParameter("@TargetCurrencyId", exchangeRate.TargetCurrency.Id),
            new SqliteParameter("@Rate", exchangeRate.Rate)
        };

        var affectedRows = await db.ExecuteAsync(commandText, parameters);

        var isUpdated = affectedRows > 0;

        return isUpdated
            ? Result<ExchangeRate>.Success(exchangeRate)
            : Result<ExchangeRate>.Failure("[DB] Update exchangeRate error: conflict/not found.");
    }
}