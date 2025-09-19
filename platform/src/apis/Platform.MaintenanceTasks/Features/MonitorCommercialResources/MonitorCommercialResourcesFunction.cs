using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Platform.MaintenanceTasks.Features.MonitorCommercialResources.Services;

namespace Platform.MaintenanceTasks.Features.MonitorCommercialResources;

public class MonitorCommercialResourcesFunction(ILogger<MonitorCommercialResourcesFunction> logger,
    IMonitorCommercialResourcesService service,
    TelemetryClient telemetry)
{
    [Function("MonitorCommercialResourcesFunction")]
    public async Task RunAsync([TimerTrigger("%schedule%")] TimerInfo timer)
    {
        using (logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.MonitorCommercialResources }
               }))
        {
            var resources = await service.GetResourcesAsync();

            foreach (var resource in resources)
            {
                var result = await service.CheckResourceAsync(resource);

                telemetry.TrackEvent("commercial-resource-check", new Dictionary<string, string>
                {
                    { "Title", result.Title },
                    { "Url", result.Url },
                    { "StatusCode", result.StatusCode?.ToString() ?? "none" },
                    { "Success", result.Success.ToString() },
                    { "RedirectLocation", result.RedirectLocation ?? "none" },
                    { "Exception", result.Exception?.ToString() ?? "none" }
                });

                switch (result)
                {
                    case { Success: true }:
                        logger.LogInformation("Resource OK: {Title}", resource.Title);
                        break;
                    case { Exception: not null }:
                        logger.LogError(result.Exception, "Resource check exception: {Title} ({Url}", result.Title, result.Url);
                        break;
                    default:
                        logger.LogWarning("Resource failed: {Title} ({Url}) - Status: {StatusCode}", result.Title, result.Url, result.StatusCode);
                        break;
                }
            }
        }
    }
}