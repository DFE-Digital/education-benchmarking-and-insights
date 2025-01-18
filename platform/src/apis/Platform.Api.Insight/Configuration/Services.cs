using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using FluentValidation;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Insight.BudgetForecast;
using Platform.Api.Insight.Expenditure;
using Platform.Api.Insight.Features.Balance.Services;
using Platform.Api.Insight.Features.Census.Parameters;
using Platform.Api.Insight.Features.Census.Services;
using Platform.Api.Insight.Features.Census.Validators;
using Platform.Api.Insight.Features.Income.Parameters;
using Platform.Api.Insight.Features.Income.Services;
using Platform.Api.Insight.Features.Income.Validators;
using Platform.Api.Insight.MetricRagRatings;
using Platform.Api.Insight.Schools;
using Platform.Api.Insight.Trusts;
using Platform.Api.Insight.Validators;
using Platform.Cache.Configuration;
using Platform.Functions;
using Platform.Json;
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
            .AddSqlServer(sqlConnString)
            .AddRedis();

        serviceCollection
            .AddSingleton<IFunctionContextDataProvider, FunctionContextDataProvider>()
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
            .AddTransient<IValidator<ExpenditureNationalAvgParameters>, ExpenditureNationalAvgParametersValidator>()
            .AddTransient<IValidator<QuerySchoolExpenditureParameters>, QuerySchoolExpenditureParametersValidator>()
            .AddTransient<IValidator<QueryTrustExpenditureParameters>, QueryTrustExpenditureParametersValidator>()
            .AddTransient<IValidator<IncomeParameters>, IncomeParametersValidator>()
            .AddTransient<IValidator<MetricRagRatingsParameters>, MetricRagRatingsParametersValidator>()
            .AddTransient<IValidator<CensusParameters>, CensusParametersValidator>()
            .AddTransient<IValidator<CensusNationalAvgParameters>, CensusNationalAvgParametersValidator>()
            .AddTransient<IValidator<CensusQuerySchoolsParameters>, CensusQuerySchoolsParametersValidator>();

        serviceCollection.AddRedis();

        //TODO: Add serilog configuration AB#227696
        var sqlTelemetryEnabled = Environment.GetEnvironmentVariable("Sql__TelemetryEnabled");
        serviceCollection
            .AddApplicationInsightsTelemetryWorkerService()
            .ConfigureFunctionsApplicationInsights()
            .ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, _) =>
            {
                module.EnableSqlCommandTextInstrumentation = bool.TrueString.Equals(sqlTelemetryEnabled, StringComparison.OrdinalIgnoreCase);
            });

        serviceCollection.Configure<JsonSerializerOptions>(SystemTextJsonExtensions.Options);
    }
}