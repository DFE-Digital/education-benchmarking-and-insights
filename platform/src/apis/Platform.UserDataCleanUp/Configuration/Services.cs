using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Platform.Functions.Extensions;
using Platform.Sql;
namespace Platform.UserDataCleanUp.Configuration;

[ExcludeFromCodeCoverage]
internal static class Services
{
    internal static void Configure(IServiceCollection serviceCollection)
    {
        var sqlConnString = Environment.GetEnvironmentVariable("Sql__ConnectionString");
        ArgumentNullException.ThrowIfNull(sqlConnString);
        
        serviceCollection
            .AddSingleton<IDatabaseFactory>(new DatabaseFactory(sqlConnString))
            .AddSingleton<IPlatformDb, PlatformDb>();

        //TODO: Add serilog configuration AB#227696
        serviceCollection
            .AddApplicationInsightsTelemetryWorkerService()
            .ConfigureFunctionsApplicationInsights();

        serviceCollection.Configure<JsonSerializerOptions>(JsonExtensions.Options);
    }
}