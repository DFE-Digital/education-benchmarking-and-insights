using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Platform.Json;
using Platform.MaintenanceTasks.Features.MonitorCommercialResources;
using Platform.MaintenanceTasks.Features.UserDataCleanUp;
using Platform.Sql;

// ReSharper disable UnusedMethodReturnValue.Local

namespace Platform.MaintenanceTasks.Configuration;

[ExcludeFromCodeCoverage]
internal static class Services
{
    internal static void Configure(HostBuilderContext context, IServiceCollection serviceCollection)
    {
        var configuration = context.Configuration;

        serviceCollection
            .AddPlatformServices(configuration)
            .AddTelemetry()
            .AddFeatures();

        serviceCollection.Configure<JsonSerializerOptions>(SystemTextJsonExtensions.Options);
    }

    private static IServiceCollection AddTelemetry(this IServiceCollection serviceCollection)
    {
        var sqlTelemetryEnabled = Environment.GetEnvironmentVariable("Sql__TelemetryEnabled");
        serviceCollection
            .AddApplicationInsightsTelemetryWorkerService()
            .ConfigureFunctionsApplicationInsights()
            .ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, _) =>
            {
                module.EnableSqlCommandTextInstrumentation = bool.TrueString.Equals(sqlTelemetryEnabled, StringComparison.OrdinalIgnoreCase);
            });

        return serviceCollection;
    }

    private static IServiceCollection AddPlatformServices(this IServiceCollection serviceCollection, IConfiguration configuration) => serviceCollection
        .AddPlatformSql(configuration);

    private static IServiceCollection AddFeatures(this IServiceCollection services) => services
        .AddUserDataCleanUpFeature()
        .AddMonitorCommercialResourcesFeature();
}