using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Platform.Cache.Configuration;
using Platform.Functions.Extensions;
using Platform.Orchestrator.Search;
using Platform.Sql;
namespace Platform.Orchestrator.Configuration;

[ExcludeFromCodeCoverage]
internal static class Services
{
    internal static void Configure(IServiceCollection serviceCollection)
    {
        var sqlConnString = Environment.GetEnvironmentVariable("Sql__ConnectionString");
        var searchName = Environment.GetEnvironmentVariable("Search__Name");
        var searchKey = Environment.GetEnvironmentVariable("Search__Key");

        ArgumentNullException.ThrowIfNull(sqlConnString);
        ArgumentNullException.ThrowIfNull(searchName);
        ArgumentNullException.ThrowIfNull(searchKey);

        serviceCollection
            .AddSingleton<IDatabaseFactory>(new DatabaseFactory(sqlConnString))
            .AddSingleton<IPipelineDb, PipelineDb>()
            .AddSingleton<ISearchIndexerClient, SearchIndexerClient>()
            .AddSingleton<IPipelineSearch, PipelineSearch>();

        //TODO: Add serilog configuration AB#227696
        var sqlTelemetryEnabled = Environment.GetEnvironmentVariable("Sql__TelemetryEnabled");
        serviceCollection
            .AddApplicationInsightsTelemetryWorkerService()
            .ConfigureFunctionsApplicationInsights()
            .ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, _) =>
            {
                module.EnableSqlCommandTextInstrumentation = bool.TrueString.Equals(sqlTelemetryEnabled, StringComparison.OrdinalIgnoreCase);
            });

        serviceCollection.Configure<JsonSerializerOptions>(JsonExtensions.Options);

        serviceCollection.AddOptions<PipelineSearchOptions>().Configure(x =>
        {
            x.Name = searchName;
            x.Key = searchKey;
        });

        serviceCollection.AddRedis();
    }
}