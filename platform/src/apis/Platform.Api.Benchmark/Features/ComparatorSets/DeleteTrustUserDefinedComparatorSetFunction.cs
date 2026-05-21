using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Benchmark.Features.ComparatorSets.Services;
using Platform.Functions.Extensions;
using Platform.OpenApi;
using Platform.OpenApi.Attributes;

namespace Platform.Api.Benchmark.Features.ComparatorSets;

public class DeleteTrustUserDefinedComparatorSetFunction(IComparatorSetsService service)
{
    [Function(nameof(DeleteTrustUserDefinedComparatorSetFunction))]
    [OpenApiOperation(nameof(DeleteTrustUserDefinedComparatorSetFunction), Constants.Features.ComparatorSets)]
    [OpenApiCompanyNumberParameter]
    [OpenApiIdentifierParameter]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithoutBody(HttpStatusCode.OK)]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "delete", Route = Routes.TrustUserDefinedComparatorSetItem)] HttpRequestData req,
        string companyNumber,
        string identifier,
        CancellationToken cancellationToken = default)
    {
        var comparatorSet = await service.UserDefinedTrustAsync(companyNumber, identifier, cancellationToken: cancellationToken);
        if (comparatorSet == null)
        {
            return req.CreateNotFoundResponse();
        }

        await service.DeleteTrustAsync(comparatorSet);
        return req.CreateResponse(HttpStatusCode.OK);
    }
}
