using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EducationBenchmarking.Platform.Api.Schools;

/// <summary>
/// 
/// </summary>
[ApiExplorerSettings(GroupName = "Health Check")]
public class HealthCheckFunctions
{
    private readonly HealthCheckService _healthCheck;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="healthCheck"></param>
    public HealthCheckFunctions(HealthCheckService healthCheck)
    {
        _healthCheck = healthCheck;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    [FunctionName(nameof(Health))]
    public async Task<IActionResult> Health(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")] HttpRequest req)
    {
        var healthStatus = await _healthCheck.CheckHealthAsync();
        return new OkObjectResult(Enum.GetName(typeof(HealthStatus), healthStatus.Status));
    }
}