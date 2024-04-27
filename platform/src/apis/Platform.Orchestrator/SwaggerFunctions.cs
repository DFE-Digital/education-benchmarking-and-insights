using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using AzureFunctions.Extensions.Swashbuckle;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace Platform.Orchestrator;

[ExcludeFromCodeCoverage]
public static class SwaggerFunctions
{
    [SwaggerIgnore]
    [FunctionName("Swagger")]
    public static HttpResponseMessage Swagger(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger/json")] HttpRequestMessage req,
        [SwashBuckleClient] ISwashBuckleClient swashBuckleClient)
    {
        return swashBuckleClient.CreateSwaggerJsonDocumentResponse(req);
    }

    [SwaggerIgnore]
    [FunctionName("SwaggerUI")]
    public static HttpResponseMessage SwaggerUi(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger/ui")] HttpRequestMessage req,
        [SwashBuckleClient] ISwashBuckleClient swashBuckleClient)
    {
        return swashBuckleClient.CreateSwaggerUIResponse(req, "swagger/json");
    }
}