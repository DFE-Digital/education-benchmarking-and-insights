using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Establishment.Features.Trusts;

public class TrustComparatorsFunctions(ITrustComparatorsService service)
{
    //TODO : Consider request validation
    [Function(nameof(TrustComparatorsAsync))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(TrustComparatorsAsync), Constants.Features.Trusts)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(TrustComparatorsRequest), Description = "The comparator characteristics object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(TrustComparators))]
    public async Task<HttpResponseData> TrustComparatorsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = "trust/{identifier}/comparators")]
        HttpRequestData req,
        string identifier)
    {
        var body = await req.ReadAsJsonAsync<TrustComparatorsRequest>();
        var comparators = await service.ComparatorsAsync(identifier, body);
        return await req.CreateJsonResponseAsync(comparators);
    }
}