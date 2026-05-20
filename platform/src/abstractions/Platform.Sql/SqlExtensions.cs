using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Platform.Sql;

[ExcludeFromCodeCoverage]
public static class SqlExtensions
{
    public static IServiceCollection AddPlatformSql(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var options = GetOptions(configuration);

        serviceCollection
            .AddSingleton(Options.Create(options))
            .AddSingleton<IDatabaseFactory, DatabaseFactory>();

        return serviceCollection;
    }

    public static IHealthChecksBuilder AddPlatformSql(this IHealthChecksBuilder builder, IConfiguration configuration)
    {
        var options = GetOptions(configuration);

        builder.AddSqlServer(options.ConnectionString);

        return builder;
    }

    private static PlatformSqlOptions GetOptions(IConfiguration configuration)
    {
        var conn = configuration["Sql__ConnectionString"];
        ArgumentNullException.ThrowIfNull(conn);

        return new PlatformSqlOptions(conn);
    }
}

[ExcludeFromCodeCoverage]
public class PlatformSqlOptions(string connectionString)
{
    public string ConnectionString => connectionString;
}
