using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Insight.Features.BudgetForecast.Services;

namespace Platform.Api.Insight.Features.BudgetForecast;

[ExcludeFromCodeCoverage]
public static class BudgetForecastFeature
{
    public static IServiceCollection AddBudgetForecastFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IBudgetForecastService, BudgetForecastService>();

        return serviceCollection;
    }
}