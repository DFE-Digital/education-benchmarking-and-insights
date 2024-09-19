using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Azure;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Benchmark.Comparators;
using Platform.Api.Benchmark.ComparatorSets;
using Platform.Api.Benchmark.CustomData;
using Platform.Api.Benchmark.FinancialPlans;
using Platform.Api.Benchmark.UserData;
using Platform.Functions.Extensions;
using Platform.Infrastructure;
using Platform.Search;
using Platform.Sql;

namespace Platform.Api.Benchmark.Configuration;

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
            .AddHealthChecks()
            .AddSqlServer(sqlConnString);

        var searchEndpoint = new Uri($"https://{searchName}.search.windows.net/");
        var searchCredential = new AzureKeyCredential(searchKey);
        
        serviceCollection
            .AddSingleton<IDatabaseFactory>(new DatabaseFactory(sqlConnString))
            .AddSingleton<ISearchConnection<ComparatorSchool>>(new SearchConnection<ComparatorSchool>(searchEndpoint, searchCredential, ResourceNames.Search.Indexes.SchoolComparators))
            .AddSingleton<ISearchConnection<ComparatorTrust>>(new SearchConnection<ComparatorTrust>(searchEndpoint, searchCredential, ResourceNames.Search.Indexes.TrustComparators))
            .AddSingleton<IComparatorSetsService, ComparatorSetsService>()
            .AddSingleton<IFinancialPlansService, FinancialPlansService>()
            .AddSingleton<IComparatorSchoolsService, ComparatorSchoolsService>()
            .AddSingleton<IComparatorTrustsService, ComparatorTrustsService>()
            .AddSingleton<IUserDataService, UserDataService>()
            .AddSingleton<ICustomDataService, CustomDataService>();

        serviceCollection
            .AddTransient<IValidator<ComparatorSetUserDefinedSchool>, ComparatorSetUserDefinedSchoolValidator>()
            .AddTransient<IValidator<ComparatorSetUserDefinedTrust>, ComparatorSetUserDefinedTrustValidator>();

        //TODO: Add serilog configuration AB#227696
        serviceCollection
            .AddApplicationInsightsTelemetryWorkerService()
            .ConfigureFunctionsApplicationInsights();

        serviceCollection.Configure<JsonSerializerOptions>(JsonExtensions.Options);
    }
}