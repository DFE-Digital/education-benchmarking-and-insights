using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Content.Features.CommercialResources.Handlers;
using Platform.Api.Content.Features.CommercialResources.Services;
using Platform.Functions;

namespace Platform.Api.Content.Features.CommercialResources;

[ExcludeFromCodeCoverage]
public static class CommercialResourcesFeature
{
    public static IServiceCollection AddCommercialResourcesFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IGetCommercialResourcesHandler, GetCommercialResourcesV1Handler>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetCommercialResourcesHandler>, VersionedHandlerDispatcher<IGetCommercialResourcesHandler>>()
            .AddSingleton<ICommercialResourcesService, CommercialResourcesService>();

        return serviceCollection;
    }
}