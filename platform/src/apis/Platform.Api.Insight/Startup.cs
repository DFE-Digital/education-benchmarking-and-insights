using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Insight;
using Platform.Api.Insight.Balance;
using Platform.Api.Insight.Census;
using Platform.Api.Insight.Expenditure;
using Platform.Api.Insight.Income;
using Platform.Api.Insight.MetricRagRatings;
using Platform.Api.Insight.Schools;
using Platform.Api.Insight.Trusts;
using Platform.Functions.Extensions;
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

        builder.Services.AddSingleton<IDatabaseFactory, DatabaseFactory>();

        builder.Services.AddSingleton<IMetricRagRatingsService, MetricRagRatingsService>();
        builder.Services.AddSingleton<ICensusService, CensusService>();
        builder.Services.AddSingleton<IBalanceService, BalanceService>();
        builder.Services.AddSingleton<ISchoolsService, SchoolsService>();
        builder.Services.AddSingleton<ITrustsService, TrustsService>();
        builder.Services.AddSingleton<IExpenditureService, ExpenditureService>();
        builder.Services.AddSingleton<IIncomeService, IncomeService>();
    }
}