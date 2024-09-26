using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Functions.OpenApi;
namespace Platform.Api.Benchmark.Comparators;

public class ComparatorsFunctions
{
    [Function(nameof(SchoolComparatorsAsync))]
    [OpenApiOperation(nameof(SchoolComparatorsAsync), "Comparators", Deprecated = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithoutBody(HttpStatusCode.Gone)]
    public HttpResponseData SchoolComparatorsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "comparators/schools")] HttpRequestData req) => req.CreateResponse(HttpStatusCode.Gone);

    [Function(nameof(TrustComparatorsAsync))]
    [OpenApiOperation(nameof(TrustComparatorsAsync), "Comparators", Deprecated = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithoutBody(HttpStatusCode.Gone)]
    public HttpResponseData TrustComparatorsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "comparators/trusts")] HttpRequestData req) => req.CreateResponse(HttpStatusCode.Gone);
}