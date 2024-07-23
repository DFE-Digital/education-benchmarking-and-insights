using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Establishment.LocalAuthorities;
using Platform.Api.Establishment.Schools;
using Platform.Api.Establishment.Trusts;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Search;
using Platform.Infrastructure.Sql;
namespace Platform.Api.Establishment.Configuration;

[ExcludeFromCodeCoverage]
internal static class Services
{
    internal static void Configure(IServiceCollection serviceCollection)
    {
        var sql = Environment.GetEnvironmentVariable("Sql__ConnectionString");
        ArgumentNullException.ThrowIfNull(sql);

        serviceCollection
            .AddSerilogLoggerProvider(Constants.ApplicationName);

        serviceCollection
            .AddHealthChecks()
            .AddSqlServer(sql);

        serviceCollection
            .AddOptions<SqlDatabaseOptions>()
            .BindConfiguration("Sql")
            .ValidateDataAnnotations();

        serviceCollection
            .AddOptions<SearchServiceOptions>()
            .BindConfiguration("Search")
            .ValidateDataAnnotations();

        serviceCollection
            .AddSingleton<IDatabaseFactory, DatabaseFactory>()
            .AddSingleton<ISchoolsService, SchoolsService>()
            .AddSingleton<ITrustsService, TrustsService>()
            .AddSingleton<ILocalAuthoritiesService, LocalAuthoritiesService>();

        serviceCollection
            .AddTransient<IValidator<SuggestRequest>, PostSuggestRequestValidator>();

        serviceCollection
            .AddApplicationInsightsTelemetryWorkerService()
            .ConfigureFunctionsApplicationInsights();

        serviceCollection.Configure<JsonSerializerOptions>(JsonExtensions.Options);
    }
}