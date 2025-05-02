using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Insight.Features.Schools.Models;
using Platform.Api.Insight.Features.Schools.Services;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Insight.Features.Schools;

public class GetSchoolCharacteristicsFunction(ISchoolsService service)
{
    [Function(nameof(GetSchoolCharacteristicsFunction))]
    [OpenApiOperation(nameof(GetSchoolCharacteristicsFunction), Constants.Features.Schools)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(SchoolCharacteristic))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.SchoolCharacteristics)] HttpRequestData req,
        string urn,
        CancellationToken cancellationToken = default)
    {
        var result = await service.CharacteristicAsync(urn, cancellationToken);
        return result == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(result, cancellationToken: cancellationToken);
    }
}