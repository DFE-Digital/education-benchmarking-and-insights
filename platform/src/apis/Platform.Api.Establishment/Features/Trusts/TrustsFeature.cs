using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Establishment.Features.Trusts.Services;

namespace Platform.Api.Establishment.Features.Trusts;

[ExcludeFromCodeCoverage]
public static class TrustsFeature
{
    public static IServiceCollection AddTrustsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<ITrustsService, TrustsService>()
            .AddSingleton<ITrustComparatorsService, TrustComparatorsService>();

        return serviceCollection;
    }
}