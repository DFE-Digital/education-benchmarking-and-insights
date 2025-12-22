using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Trust.Features.Search.Handlers;
using Platform.Api.Trust.Features.Search.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;
using Platform.Search;

namespace Platform.Api.Trust.Features.Search.Functions;

public class PostSuggestFunction(IVersionedHandlerDispatcher<IPostSuggestHandler> dispatcher) : VersionedFunctionBase<IPostSuggestHandler>(dispatcher)
{
    [Function(nameof(PostSuggestFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(PostSuggestFunction), Constants.Features.Search)]
    [OpenApiParameter(Platform.Functions.Constants.ApiVersion, Type = typeof(string), Required = false, In = ParameterLocation.Header)]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(TrustSuggestRequest), Description = "The suggest object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SuggestResponse<TrustSummaryResponse>))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "Validation errors or bad request.")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = Routes.Suggest)] HttpRequestData req,
        CancellationToken token = default)
    {
        return await WithHandlerAsync(
            req,
            handler => handler.HandleAsync(req, token),
            token);
    }
}