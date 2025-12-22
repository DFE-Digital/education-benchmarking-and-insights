using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Trust.Features.Comparators.Handlers;
using Platform.Api.Trust.Features.Comparators.Services;
using Platform.Functions;

namespace Platform.Api.Trust.Features.Comparators;

[ExcludeFromCodeCoverage]
public static class ServiceCollection
{
    public static IServiceCollection AddComparatorsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IPostComparatorsHandler, PostComparatorsV1Handler>()
            .AddSingleton<IVersionedHandlerDispatcher<IPostComparatorsHandler>, VersionedHandlerDispatcher<IPostComparatorsHandler>>()
            .AddSingleton<IComparatorsService, ComparatorsService>();

        return serviceCollection;
    }
}