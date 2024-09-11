using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Sql;
namespace Platform.UserDataCleanUp.Configuration;

[ExcludeFromCodeCoverage]
internal static class Services
{
    internal static void Configure(IServiceCollection serviceCollection)
    {
        var sql = Environment.GetEnvironmentVariable("Sql__ConnectionString");
        ArgumentNullException.ThrowIfNull(sql);

        serviceCollection
            .AddOptions<SqlDatabaseOptions>()
            .BindConfiguration("Sql");

        serviceCollection
            .AddSingleton<IDatabaseFactory, DatabaseFactory>()
            .AddSingleton<IPlatformDb, PlatformDb>();

        //TODO: Add serilog configuration AB#227696
        serviceCollection
            .AddApplicationInsightsTelemetryWorkerService()
            .ConfigureFunctionsApplicationInsights();

        serviceCollection.Configure<JsonSerializerOptions>(JsonExtensions.Options);
    }
}