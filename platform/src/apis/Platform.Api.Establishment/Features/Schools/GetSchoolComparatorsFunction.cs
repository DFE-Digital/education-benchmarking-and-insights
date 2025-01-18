using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Establishment.Features.Schools.Models;
using Platform.Api.Establishment.Features.Schools.Requests;
using Platform.Api.Establishment.Features.Schools.Services;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Functions;

namespace Platform.Api.Establishment.Features.Schools;

public class GetSchoolComparatorsFunction(ISchoolComparatorsService service)
{
    //TODO : Consider request validation
    [Function(nameof(GetSchoolComparatorsFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetSchoolComparatorsFunction), Constants.Features.Schools)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(SchoolComparatorsRequest), Description = "The comparator characteristics object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SchoolComparators))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = "school/{identifier}/comparators")]
        HttpRequestData req,
        string identifier)
    {
        var body = await req.ReadAsJsonAsync<SchoolComparatorsRequest>();
        var comparators = await service.ComparatorsAsync(identifier, body);
        return await req.CreateJsonResponseAsync(comparators);
    }
}