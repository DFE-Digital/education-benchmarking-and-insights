using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Content.Features.CommercialResources.Handlers;
using Platform.Api.Content.Features.CommercialResources.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Content.Features.CommercialResources;

public class GetCommercialResourcesFunction(IVersionedHandlerDispatcher<IGetCommercialResourcesHandler> dispatcher) : VersionedFunctionBase<IGetCommercialResourcesHandler>(dispatcher)
{
    [Function(nameof(GetCommercialResourcesFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetCommercialResourcesFunction), Constants.Features.CommercialResources)]
    [OpenApiParameter(Functions.Constants.ApiVersion, Type = typeof(string), In = ParameterLocation.Header)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(CommercialResource[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.CommercialResources)] HttpRequestData req,
        CancellationToken cancellationToken = default)
    {
        return await WithHandlerAsync(
            req,
            handler => handler.HandleAsync(req, cancellationToken),
            cancellationToken);
    }
}