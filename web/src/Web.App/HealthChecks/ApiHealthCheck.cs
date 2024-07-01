using Microsoft.Extensions.Diagnostics.HealthChecks;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;

namespace Web.App.HealthChecks;

public class ApiHealthCheck(IEnumerable<IHealthApi> apis) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {

        var isHealthy = true; // Your custom health check logic here

        foreach (var api in apis)
        {
            var result = await api.GetHealth().GetResultOrThrow<string>();
            if (Enum.Parse<HealthStatus>(result) != HealthStatus.Healthy)
            {
                isHealthy = false;
            }
        }

        return isHealthy
            ? HealthCheckResult.Healthy("The check indicates a healthy result.")
            : HealthCheckResult.Unhealthy("The check indicates an unhealthy result.");
    }
}