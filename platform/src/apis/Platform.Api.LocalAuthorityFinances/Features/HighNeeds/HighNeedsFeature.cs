using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Services;

namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds;

[ExcludeFromCodeCoverage]
public static class HighNeedsFeature
{
    public static IServiceCollection AddHighNeedsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IHighNeedsHistoryService, HighNeedsHistoryStubService>();

        return serviceCollection;
    }
}