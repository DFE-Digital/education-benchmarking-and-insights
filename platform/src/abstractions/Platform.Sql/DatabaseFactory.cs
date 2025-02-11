using System.Diagnostics.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Platform.Sql;

[ExcludeFromCodeCoverage]
public class DatabaseFactory(IOptions<PlatformSqlOptions> options) : IDatabaseFactory
{
    public async Task<IDatabaseConnection> GetConnection()
    {
        var conn = new DatabaseConnection(new SqlConnection(options.Value.ConnectionString));
        await conn.OpenAsync();
        return conn;
    }
}

public interface IDatabaseFactory
{
    Task<IDatabaseConnection> GetConnection();
}