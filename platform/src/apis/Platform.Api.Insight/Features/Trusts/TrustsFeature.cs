using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Insight.Features.Trusts.Services;

namespace Platform.Api.Insight.Features.Trusts;

[ExcludeFromCodeCoverage]
public static class TrustsFeature
{
    public static IServiceCollection AddTrustsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<ITrustsService, TrustsService>();

        return serviceCollection;
    }
}