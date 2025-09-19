using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Platform.MaintenanceTasks.Features.MonitorCommercialResources;

[ExcludeFromCodeCoverage]
public static class MonitorCommercialResourcesFeature
{
    public static IServiceCollection AddMonitorCommercialResourcesFeature(this IServiceCollection services)
    {
        services.AddHttpClient("NoRedirects")
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                AllowAutoRedirect = false
            });

        return services;
    }
}