using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Platform.Sql;

[ExcludeFromCodeCoverage]
public static class SqlExtensions
{
    public static IServiceCollection AddPlatformSql(this IServiceCollection serviceCollection)
    {
        var options = GetOptions();

        serviceCollection
            .AddSingleton(Options.Create(options))
            .AddSingleton<IDatabaseFactory, DatabaseFactory>();

        return serviceCollection;
    }

    public static IHealthChecksBuilder AddPlatformSql(this IHealthChecksBuilder builder)
    {
        var options = GetOptions();

        builder.AddSqlServer(options.ConnectionString);

        return builder;
    }

    private static PlatformSqlOptions GetOptions()
    {
        var conn = Environment.GetEnvironmentVariable("Sql__ConnectionString");
        ArgumentNullException.ThrowIfNull(conn);

        return new PlatformSqlOptions(conn);
    }
}

[ExcludeFromCodeCoverage]
public class PlatformSqlOptions(string connectionString)
{
    public string ConnectionString => connectionString;
}