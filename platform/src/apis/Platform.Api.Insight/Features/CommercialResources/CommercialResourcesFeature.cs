using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Insight.Features.CommercialResources.Services;

namespace Platform.Api.Insight.Features.CommercialResources;

[ExcludeFromCodeCoverage]
public static class CommercialResourcesFeature
{
    public static IServiceCollection AddCommercialResourcesFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<ICommercialResourcesService, CommercialResourcesService>();

        return serviceCollection;
    }
}