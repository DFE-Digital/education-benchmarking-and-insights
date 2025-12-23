using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Trust.Features.BudgetForecast.Handlers;
using Platform.Api.Trust.Features.BudgetForecast.Parameters;
using Platform.Api.Trust.Features.BudgetForecast.Services;
using Platform.Api.Trust.Features.BudgetForecast.Validators;
using Platform.Functions;

namespace Platform.Api.Trust.Features.BudgetForecast;

[ExcludeFromCodeCoverage]
public static class ServiceCollection
{
    public static IServiceCollection AddBudgetForecastFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IGetForecastRiskMHandler, GetForecastRiskV1Handler>()
            .AddSingleton<IGetForecastRiskMetricsHandler, GetForecastRiskMetricsV1Handler>()
            .AddSingleton<IGetItSpendingForecastHandler, GetItSpendingForecastV1Handler>()
            .AddSingleton<IQueryItSpendingHandler, QueryItSpendingV1Handler>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetForecastRiskMHandler>, VersionedHandlerDispatcher<IGetForecastRiskMHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetForecastRiskMetricsHandler>, VersionedHandlerDispatcher<IGetForecastRiskMetricsHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetItSpendingForecastHandler>, VersionedHandlerDispatcher<IGetItSpendingForecastHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IQueryItSpendingHandler>, VersionedHandlerDispatcher<IQueryItSpendingHandler>>()
            .AddSingleton<IBudgetForecastService, BudgetForecastService>()
            .AddTransient<IValidator<ItSpendingParameters>, ItSpendingParametersValidator>();

        return serviceCollection;
    }
}