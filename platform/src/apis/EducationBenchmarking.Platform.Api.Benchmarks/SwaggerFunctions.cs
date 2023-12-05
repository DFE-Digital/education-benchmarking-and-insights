using System.Net.Http;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace EducationBenchmarking.Platform.Api.Benchmarks;

/// <summary>
/// 
/// </summary>
public static class SwaggerFunctions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="req"></param>
    /// <param name="swashBuckleClient"></param>
    /// <returns></returns>
    [SwaggerIgnore]
    [FunctionName("Swagger")]
    public static Task<HttpResponseMessage> Swagger(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger/json")] HttpRequestMessage req,
        [SwashBuckleClient] ISwashBuckleClient swashBuckleClient)
    {
        return Task.FromResult(swashBuckleClient.CreateSwaggerJsonDocumentResponse(req));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="req"></param>
    /// <param name="swashBuckleClient"></param>
    /// <returns></returns>
    [SwaggerIgnore]
    [FunctionName("SwaggerUI")]
    public static Task<HttpResponseMessage> SwaggerUi(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger/ui")] HttpRequestMessage req,
        [SwashBuckleClient] ISwashBuckleClient swashBuckleClient)
    {
        return Task.FromResult(swashBuckleClient.CreateSwaggerUIResponse(req, "swagger/json"));
    }
}