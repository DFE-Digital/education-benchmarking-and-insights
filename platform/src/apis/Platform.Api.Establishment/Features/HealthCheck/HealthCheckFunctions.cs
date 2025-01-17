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
using Platform.Functions;

namespace Platform.Api.Establishment.Features.HealthCheck;

[ExcludeFromCodeCoverage]
public class HealthCheckFunctions(HealthCheckService service)
{
    [Function(nameof(HealthAsync))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(HealthAsync), Constants.Features.HealthCheck)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.TextPlain, typeof(string))]
    public async Task<HttpResponseData> HealthAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, MethodType.Get, Route = "health")] HttpRequestData req)
    {
        var healthStatus = await service.CheckHealthAsync();
        return await req.CreateObjectResponseAsync(Enum.GetName(typeof(HealthStatus), healthStatus.Status) ?? string.Empty);
    }
}