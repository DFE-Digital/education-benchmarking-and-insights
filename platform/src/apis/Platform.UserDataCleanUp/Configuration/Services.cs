﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Platform.Json;
using Platform.Sql;

namespace Platform.UserDataCleanUp.Configuration;

[ExcludeFromCodeCoverage]
internal static class Services
{
    internal static void Configure(IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddPlatformSql()
            .AddSingleton<IPlatformDb, PlatformDb>();

        //TODO: Add serilog configuration AB#227696
        var sqlTelemetryEnabled = Environment.GetEnvironmentVariable("Sql__TelemetryEnabled");
        serviceCollection
            .AddApplicationInsightsTelemetryWorkerService()
            .ConfigureFunctionsApplicationInsights()
            .ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, _) =>
            {
                module.EnableSqlCommandTextInstrumentation = bool.TrueString.Equals(sqlTelemetryEnabled, StringComparison.OrdinalIgnoreCase);
            });

        serviceCollection.Configure<JsonSerializerOptions>(SystemTextJsonExtensions.Options);
    }
}