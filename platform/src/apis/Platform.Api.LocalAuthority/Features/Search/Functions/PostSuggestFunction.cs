using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.LocalAuthority.Features.Search.Handlers;
using Platform.Api.LocalAuthority.Features.Search.Models;
using Platform.Functions;
using Platform.OpenApi;
using Platform.OpenApi.Attributes;
using Platform.Search;

namespace Platform.Api.LocalAuthority.Features.Search.Functions;

public class PostSuggestFunction(IEnumerable<IPostSuggestHandler> handlers) : VersionedFunctionBase<IPostSuggestHandler, BasicContext>(handlers)
{
    [Function(nameof(PostSuggestFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(PostSuggestFunction), Constants.Features.Search, Summary = "Suggest local authorities", Description = "Returns a list of local authority suggestions based on the provided search text.")]
    [OpenApiApiVersionParameter]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(LocalAuthoritySuggestRequest), Description = "The local authority suggest request parameters")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SuggestResponse<LocalAuthoritySummaryResponse>), Description = "The local authority suggest results")]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "The request was invalid")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = Routes.Suggest)] HttpRequestData req,
        CancellationToken token = default)
    {
        var context = new BasicContext(req, token);
        return await RunAsync(context);
    }
}