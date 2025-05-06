using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Benchmark.Features.ComparatorSets.Services;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Benchmark.Features.ComparatorSets;

public class DeleteSchoolUserDefinedComparatorSetFunction(IComparatorSetsService service)
{
    [Function(nameof(DeleteSchoolUserDefinedComparatorSetFunction))]
    [OpenApiOperation(nameof(DeleteSchoolUserDefinedComparatorSetFunction), Constants.Features.ComparatorSets)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithoutBody(HttpStatusCode.OK)]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "delete", Route = Routes.SchoolUserDefinedComparatorSetItem)] HttpRequestData req,
        string urn,
        string identifier)
    {
        var comparatorSet = await service.UserDefinedSchoolAsync(urn, identifier);
        if (comparatorSet == null)
        {
            return req.CreateNotFoundResponse();
        }

        await service.DeleteSchoolAsync(comparatorSet);
        return req.CreateResponse(HttpStatusCode.OK);
    }
}