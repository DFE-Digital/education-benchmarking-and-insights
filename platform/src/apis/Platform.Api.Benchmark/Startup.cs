using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentValidation;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Benchmark;
using Platform.Api.Benchmark.Comparators;
using Platform.Api.Benchmark.ComparatorSets;
using Platform.Api.Benchmark.CustomData;
using Platform.Api.Benchmark.FinancialPlans;
using Platform.Api.Benchmark.UserData;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Search;
using Platform.Infrastructure.Sql;

[assembly: WebJobsStartup(typeof(Startup))]

namespace Platform.Api.Benchmark;

[ExcludeFromCodeCoverage]
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.AddCustomSwashBuckle(Assembly.GetExecutingAssembly());

        var sql = Environment.GetEnvironmentVariable("Sql__ConnectionString");
        ArgumentNullException.ThrowIfNull(sql);

        builder.Services
            .AddSerilogLoggerProvider(Constants.ApplicationName);

        builder.Services
            .AddHealthChecks()
            .AddSqlServer(sql);

        builder.Services
            .AddOptions<SqlDatabaseOptions>()
            .BindConfiguration("Sql")
            .ValidateDataAnnotations();

        builder.Services
            .AddOptions<SearchServiceOptions>()
            .BindConfiguration("Search")
            .ValidateDataAnnotations();

        builder.Services
            .AddSingleton<IDatabaseFactory, DatabaseFactory>()
            .AddSingleton<IComparatorSetsService, ComparatorSetsService>()
            .AddSingleton<IFinancialPlansService, FinancialPlansService>()
            .AddSingleton<IComparatorSchoolsService, ComparatorSchoolsService>()
            .AddSingleton<IComparatorTrustsService, ComparatorTrustsService>()
            .AddSingleton<IUserDataService, UserDataService>()
            .AddSingleton<ICustomDataService, CustomDataService>();

        builder.Services
            .AddTransient<IValidator<ComparatorSetUserDefinedSchool>, ComparatorSetUserDefinedSchoolValidator>()
            .AddTransient<IValidator<ComparatorSetUserDefinedTrust>, ComparatorSetUserDefinedTrustValidator>();
    }
}