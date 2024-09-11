using System.Data;
using Microsoft.Data.Sqlite;

namespace CurrencyExchange.DAL.Commons;

public class DataBaseHelper(string connectionString)
{
    private readonly string _connectionString = connectionString;

    private SqliteConnection CreateConnection()
    {
        return new SqliteConnection(_connectionString);
    }

    public async Task<int> ExecuteAsync(string commandText, params SqliteParameter[] parameters)
    {
        await using var connection = CreateConnection();
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = commandText;
        command.Parameters.AddRange(parameters);

        return await command.ExecuteNonQueryAsync();
    }

    public async Task<T?> QuerySingleOrDefaultAsync<T>(string commandText, Func<IDataReader, T> map,
        params SqliteParameter[] parameters)
    {
        await using var connection = CreateConnection();
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = commandText;
        command.Parameters.AddRange(parameters);

        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync()) return map(reader);
        return default;
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string commandText, Func<IDataReader, T> map,
        params SqliteParameter[] parameters)
    {
        var results = new List<T>();

        await using var connection = CreateConnection();
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = commandText;
        command.Parameters.AddRange(parameters);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync()) results.Add(map(reader));

        return results;
    }
}