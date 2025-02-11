using Microsoft.Extensions.Diagnostics.HealthChecks;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;

namespace Web.App.HealthChecks;

public class ApiHealthCheck(IEnumerable<IHealthApi> apis) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var tasks = apis.Select(GetHealthStatus);
        var results = await Task.WhenAll(tasks);

        return results.All(x => x == HealthStatus.Healthy)
            ? HealthCheckResult.Healthy("The check indicates a healthy result.")
            : HealthCheckResult.Unhealthy("The check indicates an unhealthy result.");
    }

    private static async Task<HealthStatus> GetHealthStatus(IHealthApi api)
    {
        var result = await api.GetHealth().GetResultOrThrow<string>();
        return Enum.Parse<HealthStatus>(result);
    }
}