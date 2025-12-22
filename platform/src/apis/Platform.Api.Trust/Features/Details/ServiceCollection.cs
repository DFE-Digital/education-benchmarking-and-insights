using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Trust.Features.Details.Handlers;
using Platform.Api.Trust.Features.Details.Services;
using Platform.Functions;

namespace Platform.Api.Trust.Features.Details;

[ExcludeFromCodeCoverage]
public static class ServiceCollection
{
    public static IServiceCollection AddDetailsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IGetTrustHandler, GetTrustV1Handler>()
            .AddSingleton<IQueryTrustsHandler, QueryTrustsV1Handler>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetTrustHandler>, VersionedHandlerDispatcher<IGetTrustHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IQueryTrustsHandler>, VersionedHandlerDispatcher<IQueryTrustsHandler>>()
            .AddSingleton<ITrustDetailsService, TrustDetailsService>();

        return serviceCollection;
    }
}