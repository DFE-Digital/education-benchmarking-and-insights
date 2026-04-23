using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Benchmark.Features.ComparatorSets.Services;
using Platform.Api.Benchmark.OpenApi;
using Platform.Functions.Extensions;
using Platform.OpenApi;
using Platform.OpenApi.Attributes;

namespace Platform.Api.Benchmark.Features.ComparatorSets;

public class GetSchoolUserDefinedComparatorSetFunction(IComparatorSetsService service)
{
    [Function(nameof(GetSchoolUserDefinedComparatorSetFunction))]
    [OpenApiOperation(nameof(GetSchoolUserDefinedComparatorSetFunction), Constants.Features.ComparatorSets)]
    [OpenApiUrnParameter]
    [OpenApiIdentifierParameter]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IComparatorSetUserDefinedSchool))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.SchoolUserDefinedComparatorSetItem)] HttpRequestData req,
        string urn,
        string identifier,
        CancellationToken cancellationToken = default)
    {
        var comparatorSet = await service.UserDefinedSchoolAsync(urn, identifier, cancellationToken: cancellationToken);
        return comparatorSet == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(comparatorSet, cancellationToken);
    }
}