using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using FluentValidation;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Insight.BudgetForecast;
using Platform.Api.Insight.Features.Balance.Services;
using Platform.Api.Insight.Features.Census.Parameters;
using Platform.Api.Insight.Features.Census.Services;
using Platform.Api.Insight.Features.Census.Validators;
using Platform.Api.Insight.Features.Expenditure.Parameters;
using Platform.Api.Insight.Features.Expenditure.Services;
using Platform.Api.Insight.Features.Expenditure.Validators;
using Platform.Api.Insight.Features.Files.Services;
using Platform.Api.Insight.Features.Income.Parameters;
using Platform.Api.Insight.Features.Income.Services;
using Platform.Api.Insight.Features.Income.Validators;
using Platform.Api.Insight.Features.Trusts.Services;
using Platform.Api.Insight.MetricRagRatings;
using Platform.Api.Insight.Schools;
using Platform.Api.Insight.Validators;
using Platform.Cache;
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
            .AddSingleton<IBudgetForecastService, BudgetForecastService>()
            .AddSingleton<IFilesService, FilesService>();

        serviceCollection
            .AddTransient<IValidator<ExpenditureParameters>, ExpenditureParametersValidator>()
            .AddTransient<IValidator<ExpenditureNationalAvgParameters>, ExpenditureNationalAvgParametersValidator>()
            .AddTransient<IValidator<ExpenditureQuerySchoolParameters>, ExpenditureQuerySchoolParametersValidator>()
            .AddTransient<IValidator<ExpenditureQueryTrustParameters>, ExpenditureQueryTrustParametersValidator>()
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