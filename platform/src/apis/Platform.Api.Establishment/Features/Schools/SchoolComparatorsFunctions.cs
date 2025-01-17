using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Functions;

namespace Platform.Api.Establishment.Features.Schools;

public class SchoolComparatorsFunctions(ISchoolComparatorsService service)
{
    //TODO : Consider request validation
    [Function(nameof(SchoolComparatorsAsync))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(SchoolComparatorsAsync), Constants.Features.Schools)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(SchoolComparatorsRequest), Description = "The comparator characteristics object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SchoolComparators))]
    public async Task<HttpResponseData> SchoolComparatorsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = "school/{identifier}/comparators")]
        HttpRequestData req,
        string identifier)
    {
        var body = await req.ReadAsJsonAsync<SchoolComparatorsRequest>();
        var comparators = await service.ComparatorsAsync(identifier, body);
        return await req.CreateJsonResponseAsync(comparators);
    }
}