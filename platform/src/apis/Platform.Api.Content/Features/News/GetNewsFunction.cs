using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Content.Features.News.Handlers;
using Platform.Functions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Content.Features.News;

public class GetNewsFunction(IVersionedHandlerDispatcher<IGetNewsHandler> dispatcher) : VersionedFunctionBase<IGetNewsHandler>(dispatcher)
{
    [Function(nameof(GetNewsFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetNewsFunction), Constants.Features.News)]
    [OpenApiParameter(Functions.Constants.ApiVersion, Type = typeof(string), In = ParameterLocation.Header)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(Models.News[]))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.News)]
        HttpRequestData req,
        CancellationToken cancellationToken = default)
    {
        return await WithHandlerAsync(
            req,
            handler => handler.HandleAsync(req, cancellationToken),
            cancellationToken);
    }
}