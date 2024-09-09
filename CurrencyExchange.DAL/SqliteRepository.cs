using System.Data;
using Microsoft.Data.Sqlite;

namespace CurrencyExchange.DAL;

public class SqliteRepository(string connectionString)
{
    private SqliteConnection CreateConnection()
    {
        return new SqliteConnection(connectionString);
    }

    public async Task ExecuteAsync(string commandText, params SqliteParameter[] parameters)
    {
        await using var connection = CreateConnection();
        await connection.OpenAsync();
        
        await using var command = connection.CreateCommand();
        command.CommandText = commandText;
        command.Parameters.AddRange(parameters);
        
        await command.ExecuteNonQueryAsync();
    }

    public async Task<T> QuerySingleAsync<T>(string commandText, Func<IDataReader, T> map,
        params SqliteParameter[] parameters)
    {
        await using var connection = CreateConnection();
        await connection.OpenAsync();
        
        await using var command = connection.CreateCommand();
        command.CommandText = commandText;
        command.Parameters.AddRange(parameters);
        
        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return map(reader);
        }
        else
        {
            return default;
        }
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
        while (await reader.ReadAsync())
        {
            results.Add(map(reader));
        }

        return results;
    }
}