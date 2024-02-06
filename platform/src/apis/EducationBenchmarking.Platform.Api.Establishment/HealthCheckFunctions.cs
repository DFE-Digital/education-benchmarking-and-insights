using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EducationBenchmarking.Platform.Api.Establishment;

[ApiExplorerSettings(GroupName = "Health Check")]
[ExcludeFromCodeCoverage]
public class HealthCheckFunctions
{
    private readonly HealthCheckService _healthCheck;
    
    public HealthCheckFunctions(HealthCheckService healthCheck)
    {
        _healthCheck = healthCheck;
    }
    
    [FunctionName(nameof(HealthAsync))]
    public async Task<IActionResult> HealthAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")] HttpRequest req)
    {
        var healthStatus = await _healthCheck.CheckHealthAsync();
        return new OkObjectResult(Enum.GetName(typeof(HealthStatus), healthStatus.Status));
    }
}