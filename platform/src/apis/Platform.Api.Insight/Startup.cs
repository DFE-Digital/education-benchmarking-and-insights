using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Insight;
using Platform.Api.Insight.Db;
using Platform.Api.Insight.MetricRagRatings;
using Platform.Api.Insight.Schools;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Cosmos;
using Platform.Infrastructure.Sql;

[assembly: WebJobsStartup(typeof(Startup))]

namespace Platform.Api.Insight;

[ExcludeFromCodeCoverage]
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.AddCustomSwashBuckle(Assembly.GetExecutingAssembly());

        builder.Services.AddSerilogLoggerProvider(Constants.ApplicationName);
        builder.Services.AddHealthChecks();

        builder.Services.AddOptions<SqlDatabaseOptions>().BindConfiguration("Sql").ValidateDataAnnotations();
        builder.Services.AddOptions<SchoolFinancesDbOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();
        builder.Services.AddOptions<FinancesDbOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();
        builder.Services.AddOptions<TrustFinancesDbOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();
        builder.Services.AddOptions<SchoolsDbOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();
        builder.Services.AddOptions<CosmosDatabaseOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();

        builder.Services.AddSingleton<ICosmosClientFactory, CosmosClientFactory>();
        builder.Services.AddSingleton<IDatabaseFactory, DatabaseFactory>();

        builder.Services.AddSingleton<ISchoolFinancesDb, SchoolFinancesDb>();
        builder.Services.AddSingleton<ITrustFinancesDb, TrustFinancesDb>();
        builder.Services.AddSingleton<IMetricRagRatingsService, MetricRagRatingsService>();
        builder.Services.AddSingleton<ICensusDb, CensusDb>();
        builder.Services.AddSingleton<IIncomeDb, IncomeDb>();
        builder.Services.AddSingleton<IBalanceDb, BalanceDb>();
        builder.Services.AddSingleton<ISchoolsDb, SchoolsDb>();
        builder.Services.AddSingleton<ISchoolMetricsDb, SchoolMetricsDb>();
        builder.Services.AddSingleton<ISchoolsService, SchoolsService>();
    }
}