using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
namespace Platform.Infrastructure.Sql;

[ExcludeFromCodeCoverage]
public record SqlDatabaseOptions
{
    [Required] public string? ConnectionString { get; set; }
}

[ExcludeFromCodeCoverage]
public class DatabaseFactory(IOptions<SqlDatabaseOptions> options) : IDatabaseFactory
{
    private readonly SqlDatabaseOptions _options = options.Value;
    public async Task<IDatabaseConnection> GetConnection()
    {
        var conn = new DatabaseConnection(new SqlConnection(_options.ConnectionString));
        await conn.OpenAsync();
        return conn;
    }
}

public interface IDatabaseFactory
{
    Task<IDatabaseConnection> GetConnection();
}