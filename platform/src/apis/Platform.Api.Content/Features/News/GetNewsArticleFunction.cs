using System.Collections.Generic;
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

public class GetNewsArticleFunction(IEnumerable<IGetNewsArticleHandler> handlers) : VersionedFunctionBase<IGetNewsArticleHandler, IdContext>(handlers)
{
    [Function(nameof(GetNewsArticleFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetNewsArticleFunction), Constants.Features.News)]
    [OpenApiParameter("slug", Type = typeof(string), Required = true)]
    [OpenApiParameter(Functions.Constants.ApiVersion, Type = typeof(string), In = ParameterLocation.Header)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(Models.News))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.NewsArticle)] HttpRequestData req,
        string slug,
        CancellationToken token = default)
    {
        var context = new IdContext(req, token, slug);
        return await RunAsync(context);
    }
}