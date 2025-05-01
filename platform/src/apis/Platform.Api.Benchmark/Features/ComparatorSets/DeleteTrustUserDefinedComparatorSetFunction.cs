using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Benchmark.Features.ComparatorSets.Services;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Benchmark.Features.ComparatorSets;

public class DeleteTrustUserDefinedComparatorSetFunction(IComparatorSetsService service)
{
    [Function(nameof(DeleteTrustUserDefinedComparatorSetFunction))]
    [OpenApiOperation(nameof(DeleteTrustUserDefinedComparatorSetFunction), Constants.Features.ComparatorSets)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithoutBody(HttpStatusCode.OK)]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "delete", Route = Routes.TrustUserDefinedComparatorSetItem)] HttpRequestData req,
        string companyNumber,
        string identifier)
    {
        var comparatorSet = await service.UserDefinedTrustAsync(companyNumber, identifier);
        if (comparatorSet == null)
        {
            return req.CreateNotFoundResponse();
        }

        await service.DeleteTrustAsync(comparatorSet);
        return req.CreateResponse(HttpStatusCode.OK);
    }
}