using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.LocalAuthority.Features.Details.Handlers;
using Platform.Api.LocalAuthority.Features.Details.Services;
using Platform.Functions;

namespace Platform.Api.LocalAuthority.Features.Details;

[ExcludeFromCodeCoverage]
public static class ServiceCollection
{
    public static IServiceCollection AddDetailsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IGetLocalAuthorityHandler, GetLocalAuthorityV1Handler>()
            .AddSingleton<IQueryLocalAuthoritiesHandler, QueryLocalAuthoritiesV1Handler>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetLocalAuthorityHandler>, VersionedHandlerDispatcher<IGetLocalAuthorityHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IQueryLocalAuthoritiesHandler>, VersionedHandlerDispatcher<IQueryLocalAuthoritiesHandler>>()
            .AddSingleton<ILocalAuthorityDetailsService, LocalAuthorityDetailsService>();

        return serviceCollection;
    }
}