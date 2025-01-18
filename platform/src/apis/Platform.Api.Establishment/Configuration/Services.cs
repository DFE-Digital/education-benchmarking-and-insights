using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using FluentValidation;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Establishment.Features.LocalAuthorities;
using Platform.Api.Establishment.Features.LocalAuthorities.Services;
using Platform.Api.Establishment.Features.Schools;
using Platform.Api.Establishment.Features.Schools.Services;
using Platform.Api.Establishment.Features.Trusts;
using Platform.Api.Establishment.Features.Trusts.Services;
using Platform.Functions.Middleware;
using Platform.Json;
using Platform.Search;
using Platform.Sql;

namespace Platform.Api.Establishment.Configuration;

[ExcludeFromCodeCoverage]
internal static class Services
{
    internal static void Configure(IServiceCollection serviceCollection)
    {
        var sqlConnString = Environment.GetEnvironmentVariable("Sql__ConnectionString");

        ArgumentNullException.ThrowIfNull(sqlConnString);

        serviceCollection
            .AddHealthChecks()
            .AddSqlServer(sqlConnString)
            .AddSearch();

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

        serviceCollection
            .AddSingleton<IExceptionHandlingDataProvider, ExceptionHandlingDataProvider>()
            .AddSingleton<IDatabaseFactory>(new DatabaseFactory(sqlConnString))
            .AddSingleton<ISchoolsService, SchoolsService>()
            .AddSingleton<ITrustsService, TrustsService>()
            .AddSingleton<ILocalAuthoritiesService, LocalAuthoritiesService>()
            .AddSingleton<ISchoolComparatorsService, SchoolComparatorsService>()
            .AddSingleton<ITrustComparatorsService, TrustComparatorsService>();

        serviceCollection
            .AddTransient<IValidator<SuggestRequest>, PostSuggestRequestValidator>();

        serviceCollection.AddSearch();

        serviceCollection.Configure<JsonSerializerOptions>(SystemTextJsonExtensions.Options);
    }
}