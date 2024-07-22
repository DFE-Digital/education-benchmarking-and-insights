using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
namespace Platform.Api.Insight;

[ExcludeFromCodeCoverage]
public class HealthCheckFunctions(HealthCheckService healthCheck)
{
    [Function(nameof(HealthAsync))]
    [OpenApiOperation(nameof(HealthAsync), "Health Check")]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "text/plain", typeof(string))]
    public async Task<HttpResponseData> HealthAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")] HttpRequestData req)
    {
        var healthStatus = await healthCheck.CheckHealthAsync();
        return await req.CreateObjectResponseAsync(Enum.GetName(typeof(HealthStatus), healthStatus.Status) ?? string.Empty);
    }
}