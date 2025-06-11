using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Content.Features.Years.Handlers;
using Platform.Api.Content.Features.Years.Services;
using Platform.Functions;

namespace Platform.Api.Content.Features.Years;

[ExcludeFromCodeCoverage]
public static class YearsFeature
{
    public static IServiceCollection AddYearsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IGetCurrentReturnYearsHandler, GetCurrentReturnYearsV1Handler>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetCurrentReturnYearsHandler>, VersionedHandlerDispatcher<IGetCurrentReturnYearsHandler>>()
            .AddSingleton<IYearsService, YearsService>();

        return serviceCollection;
    }
}