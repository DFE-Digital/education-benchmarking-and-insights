using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;
using Platform.Api.Establishment.Features.LocalAuthorities.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Establishment.Features.LocalAuthorities;

public class GetLocalAuthorityStatisticalNeighboursFunction(ILocalAuthoritiesService service)
{
    [Function(nameof(GetLocalAuthorityStatisticalNeighboursFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetLocalAuthorityStatisticalNeighboursFunction), Constants.Features.LocalAuthorities)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(LocalAuthorityStatisticalNeighboursResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.LocalAuthorityStatisticalNeighbours)] HttpRequestData req,
        string identifier,
        CancellationToken cancellationToken = default)
    {
        var response = await service.GetStatisticalNeighboursAsync(identifier, cancellationToken);
        if (response == null)
        {
            return req.CreateNotFoundResponse();
        }

        return await req.CreateJsonResponseAsync(response, cancellationToken: cancellationToken);
    }
}