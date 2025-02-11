using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Insight.Features.Balance.Services;

namespace Platform.Api.Insight.Features.Balance;

[ExcludeFromCodeCoverage]
public static class BalanceFeature
{
    public static IServiceCollection AddBalanceFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IBalanceService, BalanceService>();

        return serviceCollection;
    }
}