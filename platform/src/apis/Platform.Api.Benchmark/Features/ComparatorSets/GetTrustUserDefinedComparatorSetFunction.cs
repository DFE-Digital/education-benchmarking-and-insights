using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Benchmark.Features.ComparatorSets.Services;
using Platform.Api.Benchmark.OpenApi;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Benchmark.Features.ComparatorSets;

public class GetTrustUserDefinedComparatorSetFunction(IComparatorSetsService service)
{
    [Function(nameof(GetTrustUserDefinedComparatorSetFunction))]
    [OpenApiOperation(nameof(GetTrustUserDefinedComparatorSetFunction), Constants.Features.ComparatorSets)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IComparatorSetUserDefinedTrust))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.TrustUserDefinedComparatorSetItem)] HttpRequestData req,
        string companyNumber,
        string identifier)
    {
        var comparatorSet = await service.UserDefinedTrustAsync(companyNumber, identifier);
        return comparatorSet == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(comparatorSet);
    }
}