using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Benchmark.Features.CustomData.Services;

namespace Platform.Api.Benchmark.Features.CustomData;

[ExcludeFromCodeCoverage]
public static class CustomDataFeature
{
    public static IServiceCollection AddCustomDataFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<ICustomDataService, CustomDataService>();

        return serviceCollection;
    }
}