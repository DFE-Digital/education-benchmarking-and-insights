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

public class QuerySummaryFunction(IEnumerable<IQuerySummaryHandler> handlers) : VersionedFunctionBase<IQuerySummaryHandler, BasicContext>(handlers)
{
    [Function(nameof(QuerySummaryFunction))]
    [OpenApiOperation(nameof(QuerySummaryFunction), Constants.Features.MetricRagRatings, Summary = "Query summarized metric RAG ratings", Description = "Returns a summarized view of Red-Amber-Green (RAG) ratings for specified schools, filtered by local authority, trust, or specific URNs.")]
    [OpenApiUrnsParameter(Required = false)]
    [OpenApiCompanyNumberParameter]
    [OpenApiLaCodeParameter]
    [OpenApiOverallPhaseParameter(Example = typeof(OpenApiExamples.Phase))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SummaryResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "Validation errors or bad request.")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.Summary)] HttpRequestData req,
        CancellationToken token = default)
    {
        var context = new BasicContext(req, token);
        return await RunAsync(context);
    }
}