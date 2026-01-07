using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Trust.Features.Details.Handlers;
using Platform.Api.Trust.Features.Details.Services;

namespace Platform.Api.Trust.Features.Details;

[ExcludeFromCodeCoverage]
public static class ServiceCollection
{
    public static IServiceCollection AddDetailsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IGetTrustHandler, GetTrustV1Handler>()
            .AddSingleton<IQueryTrustsHandler, QueryTrustsV1Handler>()
            .AddSingleton<ITrustDetailsService, TrustDetailsService>();

        return serviceCollection;
    }
}