using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Content.Features.Years.Services;

namespace Platform.Api.Content.Features.Years;

[ExcludeFromCodeCoverage]
public static class YearsFeature
{
    public static IServiceCollection AddYearsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IYearsService, YearsService>();

        return serviceCollection;
    }
}