using System.Diagnostics.CodeAnalysis;
using Microsoft.Data.SqlClient;

namespace Platform.Sql;

[ExcludeFromCodeCoverage]
public class DatabaseFactory(string connectionString) : IDatabaseFactory
{
    public async Task<IDatabaseConnection> GetConnection()
    {
        var conn = new DatabaseConnection(new SqlConnection(connectionString));
        await conn.OpenAsync();
        return conn;
    }
}

public interface IDatabaseFactory
{
    Task<IDatabaseConnection> GetConnection();
}