using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Azure;
using Azure.Search.Documents;
using FluentValidation;
using HealthChecks.AzureSearch.DependencyInjection;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Establishment.Comparators;
using Platform.Api.Establishment.LocalAuthorities;
using Platform.Api.Establishment.Schools;
using Platform.Api.Establishment.Trusts;
using Platform.Functions.Extensions;
using Platform.Infrastructure;
using Platform.Search;
using Platform.Search.Requests;
using Platform.Search.Telemetry;
using Platform.Search.Validators;
using Platform.Sql;
namespace Platform.Api.Establishment.Configuration;

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

        var searchEndpoint = new Uri($"https://{searchName}.search.windows.net/");
        var searchCredential = new AzureKeyCredential(searchKey);

        var healthChecks = serviceCollection
            .AddHealthChecks()
            .AddSqlServer(sqlConnString);

        foreach (var index in ResourceNames.Search.Indexes.All)
        {
            healthChecks.AddAzureSearch(s =>
                {
                    s.Endpoint = searchEndpoint.AbsoluteUri;
                    s.AuthKey = searchKey;
                    s.IndexName = index;
                }, $"azuresearch:{index}");
        }

        serviceCollection
            .AddSingleton<IDatabaseFactory>(new DatabaseFactory(sqlConnString))
            .AddSingleton<ISearchConnection<LocalAuthority>>(x => new SearchConnection<LocalAuthority>(
                new SearchClient(searchEndpoint, ResourceNames.Search.Indexes.LocalAuthority, searchCredential),
                x.GetService<ITelemetryService>()))
            .AddSingleton<ISearchConnection<School>>(x => new SearchConnection<School>(
                new SearchClient(searchEndpoint, ResourceNames.Search.Indexes.School, searchCredential),
                x.GetService<ITelemetryService>()))
            .AddSingleton<ISearchConnection<Trust>>(x => new SearchConnection<Trust>(
                new SearchClient(searchEndpoint, ResourceNames.Search.Indexes.Trust, searchCredential),
                x.GetService<ITelemetryService>()))
            .AddSingleton<ISearchConnection<ComparatorSchool>>(x => new SearchConnection<ComparatorSchool>(
                new SearchClient(searchEndpoint, ResourceNames.Search.Indexes.SchoolComparators, searchCredential),
                x.GetService<ITelemetryService>()))
            .AddSingleton<ISearchConnection<ComparatorTrust>>(x => new SearchConnection<ComparatorTrust>(
                new SearchClient(searchEndpoint, ResourceNames.Search.Indexes.TrustComparators, searchCredential),
                x.GetService<ITelemetryService>()))
            .AddSingleton<ISchoolsService, SchoolsService>()
            .AddSingleton<ITrustsService, TrustsService>()
            .AddSingleton<ILocalAuthoritiesService, LocalAuthoritiesService>()
            .AddSingleton<IComparatorSchoolsService, ComparatorSchoolsService>()
            .AddSingleton<IComparatorTrustsService, ComparatorTrustsService>()
            .AddSingleton<ITelemetryService, TelemetryService>();

        serviceCollection
            .AddTransient<IValidator<SuggestRequest>, PostSuggestRequestValidator>();

        //TODO: Add serilog configuration AB#227696
        serviceCollection
            .AddApplicationInsightsTelemetryWorkerService()
            .ConfigureFunctionsApplicationInsights();

        serviceCollection.Configure<JsonSerializerOptions>(JsonExtensions.Options);
    }
}