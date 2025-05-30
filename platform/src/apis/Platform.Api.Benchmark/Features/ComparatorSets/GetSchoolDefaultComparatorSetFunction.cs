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

public class GetSchoolDefaultComparatorSetFunction(IComparatorSetsService service)
{
    [Function(nameof(GetSchoolDefaultComparatorSetFunction))]
    [OpenApiOperation(nameof(GetSchoolDefaultComparatorSetFunction), Constants.Features.ComparatorSets)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IComparatorSetSchool))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.SchoolDefaultComparatorSet)] HttpRequestData req,
        string urn,
        CancellationToken cancellationToken = default)
    {
        var comparatorSet = await service.DefaultSchoolAsync(urn, cancellationToken);
        return comparatorSet == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(comparatorSet, cancellationToken: cancellationToken);
    }
}