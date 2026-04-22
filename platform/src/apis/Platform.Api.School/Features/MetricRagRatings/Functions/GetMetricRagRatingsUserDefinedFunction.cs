using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.School.Features.MetricRagRatings.Handlers;
using Platform.Api.School.Features.MetricRagRatings.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;
using Platform.Functions.OpenApi.Attributes;

namespace Platform.Api.School.Features.MetricRagRatings.Functions;

public class GetMetricRagRatingsUserDefinedFunction(IEnumerable<IGetUserDefinedHandler> handlers) : VersionedFunctionBase<IGetUserDefinedHandler, IdContext>(handlers)
{
    [Function(nameof(GetMetricRagRatingsUserDefinedFunction))]
    [OpenApiOperation(nameof(GetMetricRagRatingsUserDefinedFunction), Constants.Features.MetricRagRatings, Summary = "Get user-defined metric RAG ratings", Description = "Returns detailed metric Red-Amber-Green (RAG) ratings for a custom set of schools defined by a user-provided identifier.")]
    [OpenApiIdentifierParameter]
    [OpenApiUseCustomDataParameter]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(DetailResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.UserDefined)] HttpRequestData req,
        string identifier,
        CancellationToken token = default)
    {
        var context = new IdContext(req, token, identifier);
        return await RunAsync(context);
    }
}