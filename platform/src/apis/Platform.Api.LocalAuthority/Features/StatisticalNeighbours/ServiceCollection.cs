using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Handlers;
using Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Services;

namespace Platform.Api.LocalAuthority.Features.StatisticalNeighbours;

[ExcludeFromCodeCoverage]
public static class ServiceCollection
{
    public static IServiceCollection AddStatisticalNeighboursFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IGetStatisticalNeighboursHandler, GetStatisticalNeighboursV1Handler>()
            .AddSingleton<IStatisticalNeighboursService, StatisticalNeighboursService>();

        return serviceCollection;
    }
}