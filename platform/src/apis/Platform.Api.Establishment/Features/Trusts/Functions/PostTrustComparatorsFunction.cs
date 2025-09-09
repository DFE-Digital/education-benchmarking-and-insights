using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Establishment.Features.Trusts.Models;
using Platform.Api.Establishment.Features.Trusts.Requests;
using Platform.Api.Establishment.Features.Trusts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Establishment.Features.Trusts;

public class PostTrustComparatorsFunction(ITrustComparatorsService service)
{
    //TODO : Consider request validation
    [Function(nameof(PostTrustComparatorsFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(PostTrustComparatorsFunction), Constants.Features.Trusts)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(TrustComparatorsRequest), Description = "The comparator characteristics object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(TrustComparators))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = Routes.TrustComparators)] HttpRequestData req,
        string identifier,
        CancellationToken cancellationToken = default)
    {
        var body = await req.ReadAsJsonAsync<TrustComparatorsRequest>(cancellationToken);
        var comparators = await service.ComparatorsAsync(identifier, body, cancellationToken);
        return await req.CreateJsonResponseAsync(comparators, cancellationToken);
    }
}