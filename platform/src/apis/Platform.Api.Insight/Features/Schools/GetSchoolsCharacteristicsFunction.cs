using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Insight.Features.Schools.Models;
using Platform.Api.Insight.Features.Schools.Parameters;
using Platform.Api.Insight.Features.Schools.Services;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Insight.Features.Schools;

public class GetSchoolsCharacteristicsFunction(ISchoolsService service)
{
    [Function(nameof(GetSchoolsCharacteristicsFunction))]
    [OpenApiOperation(nameof(GetSchoolsCharacteristicsFunction), Constants.Features.Schools)]
    [OpenApiParameter("urns", In = ParameterLocation.Query, Description = "List of school URNs", Type = typeof(string[]), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(SchoolCharacteristic[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.SchoolsCharacteristics)] HttpRequestData req)
    {
        var queryParams = req.GetParameters<SchoolsParameters>();

        var schools = await service.QueryCharacteristicAsync(queryParams.Schools);
        return await req.CreateJsonResponseAsync(schools);
    }
}