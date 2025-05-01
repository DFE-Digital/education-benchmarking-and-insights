using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Benchmark.Features.CustomData.Services;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Benchmark.Features.CustomData;

public class DeleteSchoolCustomDataFunction(ICustomDataService service)
{
    [Function(nameof(DeleteSchoolCustomDataFunction))]
    [OpenApiOperation(nameof(DeleteSchoolCustomDataFunction), "Custom Data")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithoutBody(HttpStatusCode.OK)]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "delete", Route = Routes.SchoolCustomDataItem)] HttpRequestData req,
        string urn,
        string identifier)
    {
        var data = await service.CustomDataSchoolAsync(urn, identifier);
        if (data == null)
        {
            return req.CreateNotFoundResponse();
        }

        await service.DeleteSchoolAsync(data);
        return req.CreateResponse(HttpStatusCode.OK);
    }
}