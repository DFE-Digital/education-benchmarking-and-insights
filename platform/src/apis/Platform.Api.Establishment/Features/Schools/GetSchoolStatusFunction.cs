using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Establishment.Features.Schools.Models;
using Platform.Api.Establishment.Features.Schools.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Establishment.Features.Schools;

public class GetSchoolStatusFunction(ISchoolsService service)
{
    [Function(nameof(GetSchoolStatusFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetSchoolStatusFunction), Constants.Features.Schools)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SchoolStatus))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.SchoolStatus)] HttpRequestData req,
        string identifier,
        CancellationToken cancellationToken = default)
    {
        var school = await service.GetSchoolStatusAsync(identifier, cancellationToken);

        return school == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(school, cancellationToken);
    }
}