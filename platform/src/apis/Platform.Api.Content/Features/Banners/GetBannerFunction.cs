using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Content.Features.Banners.Handlers;
using Platform.Api.Content.Features.Banners.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Content.Features.Banners;

public class GetBannerFunction(IVersionedHandlerDispatcher<IGetBannerHandler> dispatcher) : VersionedFunctionBase<IGetBannerHandler>(dispatcher)
{
    [Function(nameof(GetBannerFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetBannerFunction), Constants.Features.Banners)]
    [OpenApiParameter("target", Type = typeof(string), Required = true)]
    [OpenApiParameter(Functions.Constants.ApiVersion, Type = typeof(string), In = ParameterLocation.Header)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(Banner))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.Banner)] HttpRequestData req,
        string target,
        CancellationToken cancellationToken = default)
    {
        return await WithHandlerAsync(
            req,
            handler => handler.HandleAsync(req, target, cancellationToken),
            cancellationToken);
    }
}