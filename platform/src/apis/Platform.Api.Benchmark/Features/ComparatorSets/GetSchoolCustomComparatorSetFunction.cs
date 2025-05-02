using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Benchmark.Features.ComparatorSets.Services;
using Platform.Api.Benchmark.OpenApi;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Benchmark.Features.ComparatorSets;

public class GetSchoolCustomComparatorSetFunction(IComparatorSetsService service)
{
    [Function(nameof(GetSchoolCustomComparatorSetFunction))]
    [OpenApiOperation(nameof(GetSchoolCustomComparatorSetFunction), Constants.Features.ComparatorSets)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IComparatorSetSchool))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.SchoolCustomComparatorSet)] HttpRequestData req,
        string urn,
        string identifier,
        CancellationToken cancellationToken = default)
    {
        var comparatorSet = await service.CustomSchoolAsync(identifier, urn, cancellationToken);
        return comparatorSet == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(comparatorSet, cancellationToken: cancellationToken);
    }
}