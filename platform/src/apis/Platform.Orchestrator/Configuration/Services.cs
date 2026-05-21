using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Platform.Cache;
using Platform.Json;
using Platform.Orchestrator.Search;
using Platform.Orchestrator.Sql;
using Platform.Orchestrator.Telemetry;
using Platform.Sql;

namespace Platform.Orchestrator.Configuration;

[ExcludeFromCodeCoverage]
internal static class Services
{
    internal static void Configure(HostBuilderContext context, IServiceCollection serviceCollection)
    {
        var configuration = context.Configuration;

        var section = configuration.GetSection("Search");
        var searchName = section.GetValue<string>("Name");
        var searchKey = section.GetValue<string>("Key");

        ArgumentNullException.ThrowIfNull(searchName);
        ArgumentNullException.ThrowIfNull(searchKey);

        serviceCollection
            .AddSingleton<IPipelineDb, PipelineDb>()
            .AddSingleton<ISearchIndexerClient, SearchIndexerClient>()
            .AddSingleton<IPipelineSearch, PipelineSearch>()
            .AddSingleton<ITelemetryService, TelemetryService>();

        //TODO: Add serilog configuration AB#227696
        var sqlTelemetryEnabled = configuration.GetSection("Sql").GetValue<string>("TelemetryEnabled");
        serviceCollection
            .AddApplicationInsightsTelemetryWorkerService()
            .ConfigureFunctionsApplicationInsights()
            .ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, _) =>
            {
                module.EnableSqlCommandTextInstrumentation = bool.TrueString.Equals(sqlTelemetryEnabled, StringComparison.OrdinalIgnoreCase);
            });

        serviceCollection.Configure<JsonSerializerOptions>(SystemTextJsonExtensions.Options);

        serviceCollection.AddOptions<PipelineSearchOptions>().Configure(x =>
        {
            x.Name = searchName;
            x.Key = searchKey;
        });

        serviceCollection
            .AddPlatformSql(configuration)
            .AddPlatformCache(configuration);
    }
}
