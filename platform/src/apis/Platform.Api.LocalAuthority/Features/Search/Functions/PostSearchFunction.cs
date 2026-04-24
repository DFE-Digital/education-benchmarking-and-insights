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

public class PostSearchFunction(IEnumerable<IPostSearchHandler> handlers) : VersionedFunctionBase<IPostSearchHandler, BasicContext>(handlers)
{
    [Function(nameof(PostSearchFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(PostSearchFunction), Constants.Features.Search, Summary = "Search local authorities", Description = "Returns a list of local authorities matching the search criteria.")]
    [OpenApiApiVersionParameter]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(SearchRequest), Description = "The local authority search request parameters")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SearchResponse<LocalAuthoritySummaryResponse>), Description = "The local authority search results")]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "The request was invalid")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = Routes.Search)] HttpRequestData req,
        CancellationToken token = default)
    {
        var context = new BasicContext(req, token);
        return await RunAsync(context);
    }
}