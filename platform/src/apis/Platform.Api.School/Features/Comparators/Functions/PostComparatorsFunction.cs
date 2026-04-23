using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.School.Features.Comparators.Handlers;
using Platform.Api.School.Features.Comparators.Models;
using Platform.Functions;
using Platform.OpenApi;
using Platform.OpenApi.Attributes;

namespace Platform.Api.School.Features.Comparators.Functions;

public class PostComparatorsFunction(IEnumerable<IPostComparatorsHandler> handlers) : VersionedFunctionBase<IPostComparatorsHandler, IdContext>(handlers)
{
    [Function(nameof(PostComparatorsFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(PostComparatorsFunction), Constants.Features.Comparators, Summary = "Find similar schools", Description = "Identifies schools with similar characteristics based on the provided criteria to create a benchmarking comparator set.")]
    [OpenApiUrnParameter]
    [OpenApiApiVersionParameter]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(ComparatorsRequest), Description = "The comparator characteristics object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(ComparatorsResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = Routes.Comparators)] HttpRequestData req,
        string urn,
        CancellationToken token = default)
    {
        var context = new IdContext(req, token, urn);
        return await RunAsync(context);
    }
}