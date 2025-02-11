using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using FluentValidation;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Insight.BudgetForecast;
using Platform.Api.Insight.Features.Balance;
using Platform.Api.Insight.Features.Census;
using Platform.Api.Insight.Features.Expenditure;
using Platform.Api.Insight.Features.Files;
using Platform.Api.Insight.Features.Income;
using Platform.Api.Insight.Features.Trusts;
using Platform.Api.Insight.MetricRagRatings;
using Platform.Api.Insight.Schools;
using Platform.Api.Insight.Validators;
using Platform.Cache;
using Platform.Functions;
using Platform.Json;
using Platform.Sql;
// ReSharper disable UnusedMethodReturnValue.Local

namespace Platform.Api.Insight.Configuration;

[ExcludeFromCodeCoverage]
internal static class Services
{
    internal static void Configure(IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IFunctionContextDataProvider, FunctionContextDataProvider>()
            .AddSingleton<IMetricRagRatingsService, MetricRagRatingsService>()
            .AddSingleton<ISchoolsService, SchoolsService>()
            .AddSingleton<IBudgetForecastService, BudgetForecastService>();

        serviceCollection
            .AddTransient<IValidator<MetricRagRatingsParameters>, MetricRagRatingsParametersValidator>();

        serviceCollection
            .AddTelemetry()
            .AddHealthCheckServices()
            .AddPlatformServices()
            .AddFeatures();

        serviceCollection.Configure<JsonSerializerOptions>(SystemTextJsonExtensions.Options);
    }

    private static IServiceCollection AddHealthCheckServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddHealthChecks()
            .AddPlatformSql()
            .AddPlatformCache();

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

    private static IServiceCollection AddPlatformServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddPlatformCache()
            .AddPlatformSql();
    }

    private static IServiceCollection AddFeatures(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddBalanceFeature()
            .AddCensusFeature()
            .AddExpenditureFeature()
            .AddFilesFeature()
            .AddIncomeFeature()
            .AddTrustsFeature();
    }
}