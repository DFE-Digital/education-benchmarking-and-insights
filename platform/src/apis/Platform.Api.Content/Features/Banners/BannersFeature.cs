using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Content.Features.Banners.Handlers;
using Platform.Api.Content.Features.Banners.Services;
using Platform.Functions;

namespace Platform.Api.Content.Features.Banners;

[ExcludeFromCodeCoverage]
public static class BannersFeature
{
    public static IServiceCollection AddBannersFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IGetBannerHandler, GetBannerV1Handler>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetBannerHandler>, VersionedHandlerDispatcher<IGetBannerHandler>>()
            .AddSingleton<IBannersService, BannersService>();

        return serviceCollection;
    }
}