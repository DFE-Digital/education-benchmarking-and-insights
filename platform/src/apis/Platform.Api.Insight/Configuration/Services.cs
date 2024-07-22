using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
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
namespace Platform.Api.Insight.Configuration;

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
            .AddSingleton<IDatabaseFactory, DatabaseFactory>()
            .AddSingleton<IMetricRagRatingsService, MetricRagRatingsService>()
            .AddSingleton<ICensusService, CensusService>()
            .AddSingleton<IBalanceService, BalanceService>()
            .AddSingleton<ISchoolsService, SchoolsService>()
            .AddSingleton<ITrustsService, TrustsService>()
            .AddSingleton<IExpenditureService, ExpenditureService>()
            .AddSingleton<IIncomeService, IncomeService>()
            .AddSingleton<IBudgetForecastService, BudgetForecastService>();

        serviceCollection
            .AddApplicationInsightsTelemetryWorkerService()
            .ConfigureFunctionsApplicationInsights();

        serviceCollection.Configure<JsonSerializerOptions>(options =>
        {
            options.AllowTrailingCommas = true;
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.PropertyNameCaseInsensitive = true;
        });
    }
}