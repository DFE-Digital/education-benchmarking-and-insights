using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Insight.Features.CommercialResources.Models;
using Platform.Api.Insight.Features.CommercialResources.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Insight.Features.CommercialResources;

public class GetCommercialResourcesFunction(ICommercialResourcesService service)
{
    [Function(nameof(GetCommercialResourcesFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetCommercialResourcesFunction), Constants.Features.CommercialResources)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(CommercialResource[]))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.CommercialResources)] HttpRequestData req,
        CancellationToken cancellationToken = default)
    {
        var result = await service.GetCommercialResources(cancellationToken);
        return await req.CreateJsonResponseAsync(result, cancellationToken: cancellationToken);
    }
}