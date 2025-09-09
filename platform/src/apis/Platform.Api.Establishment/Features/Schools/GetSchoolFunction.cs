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

public class GetSchoolFunction(ISchoolsService service)
{
    [Function(nameof(GetSchoolFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetSchoolFunction), Constants.Features.Schools)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(School))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.School)] HttpRequestData req,
        string identifier,
        CancellationToken cancellationToken = default)
    {
        var school = await service.GetAsync(identifier, cancellationToken);

        return school == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(school, cancellationToken);
    }
}