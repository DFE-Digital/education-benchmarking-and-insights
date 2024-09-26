using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Benchmark.ComparatorSets;
using Platform.Api.Benchmark.CustomData;
using Platform.Api.Benchmark.FinancialPlans;
using Platform.Api.Benchmark.UserData;
using Platform.Functions.Extensions;
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

        serviceCollection
            .AddSingleton<IDatabaseFactory>(new DatabaseFactory(sqlConnString))
            .AddSingleton<IComparatorSetsService, ComparatorSetsService>()
            .AddSingleton<IFinancialPlansService, FinancialPlansService>()
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