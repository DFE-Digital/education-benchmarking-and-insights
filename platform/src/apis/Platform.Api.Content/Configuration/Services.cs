﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Content.Features.Banners;
using Platform.Api.Content.Features.CommercialResources;
using Platform.Api.Content.Features.Files;
using Platform.Api.Content.Features.Years;
using Platform.Functions;
using Platform.Json;
using Platform.Sql;

// ReSharper disable UnusedMethodReturnValue.Local

namespace Platform.Api.Content.Configuration;

[ExcludeFromCodeCoverage]
internal static class Services
{
    internal static void Configure(IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IFunctionContextDataProvider, FunctionContextDataProvider>();

        serviceCollection
            .AddTelemetry()
            .AddHealthCheckServices()
            .AddPlatformServices()
            .AddFeatures();

        serviceCollection
            .Configure<JsonSerializerOptions>(SystemTextJsonExtensions.Options);
    }

    private static IServiceCollection AddHealthCheckServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddHealthChecks()
            .AddPlatformSql();

        return serviceCollection;
    }

    private static IServiceCollection AddTelemetry(this IServiceCollection serviceCollection)
    {
        //TODO: Add serilog configuration AB#227696
        // App Insights must be BEFORE any keyed services:
        // • https://github.com/microsoft/ApplicationInsights-dotnet/issues/2828
        // • https://github.com/microsoft/ApplicationInsights-dotnet/issues/2879
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

    private static IServiceCollection AddPlatformServices(this IServiceCollection serviceCollection) => serviceCollection
        .AddPlatformSql();

    private static IServiceCollection AddFeatures(this IServiceCollection serviceCollection) => serviceCollection
        .AddBannersFeature()
        .AddCommercialResourcesFeature()
        .AddFilesFeature()
        .AddYearsFeature();
}