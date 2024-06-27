using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Insight;
using Platform.Api.Insight.Balance;
using Platform.Api.Insight.BudgetForecast;
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
            .AddSingleton<IDatabaseFactory, DatabaseFactory>()
            .AddSingleton<IMetricRagRatingsService, MetricRagRatingsService>()
            .AddSingleton<ICensusService, CensusService>()
            .AddSingleton<IBalanceService, BalanceService>()
            .AddSingleton<ISchoolsService, SchoolsService>()
            .AddSingleton<ITrustsService, TrustsService>()
            .AddSingleton<IExpenditureService, ExpenditureService>()
            .AddSingleton<IIncomeService, IncomeService>()
            .AddSingleton<IBudgetForecastService, BudgetForecastService>();
    }
}