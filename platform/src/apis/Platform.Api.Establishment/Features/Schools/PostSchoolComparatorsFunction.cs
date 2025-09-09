using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Establishment.Features.Schools.Models;
using Platform.Api.Establishment.Features.Schools.Requests;
using Platform.Api.Establishment.Features.Schools.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Establishment.Features.Schools;

public class PostSchoolComparatorsFunction(ISchoolComparatorsService service)
{
    //TODO : Consider request validation
    [Function(nameof(PostSchoolComparatorsFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(PostSchoolComparatorsFunction), Constants.Features.Schools)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(SchoolComparatorsRequest), Description = "The comparator characteristics object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SchoolComparators))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = Routes.SchoolComparators)] HttpRequestData req,
        string identifier,
        CancellationToken cancellationToken = default)
    {
        var body = await req.ReadAsJsonAsync<SchoolComparatorsRequest>(cancellationToken);
        var comparators = await service.ComparatorsAsync(identifier, body, cancellationToken);
        return await req.CreateJsonResponseAsync(comparators, cancellationToken);
    }
}