using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Establishment.Features.LocalAuthorities.Services;

namespace Platform.Api.Establishment.Features.LocalAuthorities;

[ExcludeFromCodeCoverage]
public static class LocalAuthoritiesFeature
{
    public static IServiceCollection AddLocalAuthoritiesFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<ILocalAuthoritiesService, LocalAuthoritiesService>()
            .AddSingleton<ILocalAuthorityRankingService, LocalAuthorityRankingStubService>();

        return serviceCollection;
    }
}