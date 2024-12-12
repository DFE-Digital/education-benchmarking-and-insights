using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Azure;
using Azure.Search.Documents;
using FluentValidation;
using HealthChecks.AzureSearch.DependencyInjection;
using Microsoft.ApplicationInsights.DependencyCollector;
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
            .AddKeyedSingleton(ServiceKeys.LocalAuthority, new SearchClient(searchEndpoint, ResourceNames.Search.Indexes.LocalAuthority, searchCredential))
            .AddKeyedSingleton(ServiceKeys.School, new SearchClient(searchEndpoint, ResourceNames.Search.Indexes.School, searchCredential))
            .AddKeyedSingleton(ServiceKeys.Trust, new SearchClient(searchEndpoint, ResourceNames.Search.Indexes.Trust, searchCredential))
            .AddKeyedSingleton(ServiceKeys.ComparatorSchool, new SearchClient(searchEndpoint, ResourceNames.Search.Indexes.SchoolComparators, searchCredential))
            .AddKeyedSingleton(ServiceKeys.ComparatorTrust, new SearchClient(searchEndpoint, ResourceNames.Search.Indexes.TrustComparators, searchCredential))
            .AddSingleton<ISearchConnection<LocalAuthority>>(x => new SearchConnection<LocalAuthority>(x.GetKeyedService<SearchClient>(ServiceKeys.LocalAuthority), x.GetService<ITelemetryService>()))
            .AddSingleton<ISearchConnection<School>>(x => new SearchConnection<School>(x.GetKeyedService<SearchClient>(ServiceKeys.School), x.GetService<ITelemetryService>()))
            .AddSingleton<ISearchConnection<Trust>>(x => new SearchConnection<Trust>(x.GetKeyedService<SearchClient>(ServiceKeys.Trust), x.GetService<ITelemetryService>()))
            .AddSingleton<ISearchConnection<ComparatorSchool>>(x => new SearchConnection<ComparatorSchool>(x.GetKeyedService<SearchClient>(ServiceKeys.ComparatorSchool), x.GetService<ITelemetryService>()))
            .AddSingleton<ISearchConnection<ComparatorTrust>>(x => new SearchConnection<ComparatorTrust>(x.GetKeyedService<SearchClient>(ServiceKeys.ComparatorTrust), x.GetService<ITelemetryService>()));

        serviceCollection
            .AddSingleton<IDatabaseFactory>(new DatabaseFactory(sqlConnString))
            .AddSingleton<ISchoolsService, SchoolsService>()
            .AddSingleton<ITrustsService, TrustsService>()
            .AddSingleton<ILocalAuthoritiesService, LocalAuthoritiesService>()
            .AddSingleton<IComparatorSchoolsService, ComparatorSchoolsService>()
            .AddSingleton<IComparatorTrustsService, ComparatorTrustsService>()
            .AddSingleton<ITelemetryService, TelemetryService>();

        serviceCollection
            .AddTransient<IValidator<SuggestRequest>, PostSuggestRequestValidator>();

        serviceCollection.Configure<JsonSerializerOptions>(JsonExtensions.Options);
    }

    internal static class ServiceKeys
    {
        public const string LocalAuthority = "local-authority";
        public const string School = "school";
        public const string Trust = "trust";
        public const string ComparatorSchool = "comparator-school";
        public const string ComparatorTrust = "comparator-trust";
    }
}