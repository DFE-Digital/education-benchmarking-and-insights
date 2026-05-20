using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.OpenApi;

namespace Platform.Api.School.Features.HealthCheck;

[ExcludeFromCodeCoverage]
public class GetHealthFunction(HealthCheckService healthCheck)
{
    [Function(nameof(GetHealthFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetHealthFunction), Constants.Features.HealthCheck, Summary = "Check API health status", Description = "Performs a health check on the School API to verify its operational status and dependencies.")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.TextPlain, typeof(string))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, MethodType.Get, Route = Routes.Health)] HttpRequestData req)
    {
        var healthStatus = await healthCheck.CheckHealthAsync();
        return await req.CreateObjectResponseAsync(Enum.GetName(typeof(HealthStatus), healthStatus.Status) ?? string.Empty);
    }
}
