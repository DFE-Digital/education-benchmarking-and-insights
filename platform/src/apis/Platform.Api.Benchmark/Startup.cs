using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Benchmark;
using Platform.Api.Benchmark.Comparators;
using Platform.Api.Benchmark.ComparatorSets;
using Platform.Api.Benchmark.FinancialPlans;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Cosmos;
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

        builder.Services.AddSerilogLoggerProvider(Constants.ApplicationName);
        builder.Services.AddHealthChecks();

        builder.Services.AddOptions<SqlDatabaseOptions>().BindConfiguration("Sql").ValidateDataAnnotations();
        builder.Services.AddOptions<SearchServiceOptions>().BindConfiguration("Search").ValidateDataAnnotations();

        builder.Services.AddSingleton<IDatabaseFactory, DatabaseFactory>();

        builder.Services.AddSingleton<IComparatorSetsService, ComparatorSetsService>();
        builder.Services.AddSingleton<IFinancialPlansService, FinancialPlansService>();
        builder.Services.AddSingleton<IComparatorsService, ComparatorsService>();
    }
}