using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Benchmark.Features.CustomData.Models;
using Platform.Api.Benchmark.Features.CustomData.Services;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Benchmark.Features.CustomData;

public class GetSchoolCustomDataFunction(ICustomDataService service)
{
    [Function(nameof(GetSchoolCustomDataFunction))]
    [OpenApiOperation(nameof(GetSchoolCustomDataFunction), Constants.Features.CustomData)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(CustomDataSchool))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.SchoolCustomDataItem)] HttpRequestData req,
        string urn,
        string identifier,
        CancellationToken cancellationToken = default)
    {
        var data = await service.CustomDataSchoolAsync(urn, identifier, cancellationToken);
        return data == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(data, cancellationToken: cancellationToken);
    }
}