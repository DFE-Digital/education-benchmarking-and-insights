using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using FluentValidation;
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
using Platform.Api.Insight.Validators;
using Platform.Functions.Extensions;
using Platform.Sql;
namespace Platform.Api.Insight.Configuration;

[ExcludeFromCodeCoverage]
internal static class Services
{
    internal static void Configure(IServiceCollection serviceCollection)
    {
        var sqlConnString = Environment.GetEnvironmentVariable("Sql__ConnectionString");
        ArgumentNullException.ThrowIfNull(sqlConnString);

        serviceCollection
            .AddHealthChecks()
            .AddSqlServer(sqlConnString);

        serviceCollection
            .AddSingleton<IDatabaseFactory>(new DatabaseFactory(sqlConnString))
            .AddSingleton<IMetricRagRatingsService, MetricRagRatingsService>()
            .AddSingleton<ICensusService, CensusService>()
            .AddSingleton<IBalanceService, BalanceService>()
            .AddSingleton<ISchoolsService, SchoolsService>()
            .AddSingleton<ITrustsService, TrustsService>()
            .AddSingleton<IExpenditureService, ExpenditureService>()
            .AddSingleton<IIncomeService, IncomeService>()
            .AddSingleton<IBudgetForecastService, BudgetForecastService>();

        serviceCollection
            .AddTransient<IValidator<ExpenditureParameters>, ExpenditureParametersValidator>()
            .AddTransient<IValidator<QuerySchoolExpenditureParameters>, QuerySchoolExpenditureParametersValidator>()
            .AddTransient<IValidator<QueryTrustExpenditureParameters>, QueryTrustExpenditureParametersValidator>();

        //TODO: Add serilog configuration AB#227696
        serviceCollection
            .AddApplicationInsightsTelemetryWorkerService()
            .ConfigureFunctionsApplicationInsights();

        serviceCollection.Configure<JsonSerializerOptions>(JsonExtensions.Options);
    }
}