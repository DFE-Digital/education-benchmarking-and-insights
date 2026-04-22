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

public class QueryDetailsFunction(IEnumerable<IQueryDetailsHandler> handlers) : VersionedFunctionBase<IQueryDetailsHandler, BasicContext>(handlers)
{
    [Function(nameof(QueryDetailsFunction))]
    [OpenApiOperation(nameof(QueryDetailsFunction), Constants.Features.MetricRagRatings, Summary = "Query detailed metric RAG ratings", Description = "Returns a detailed breakdown of Red-Amber-Green (RAG) ratings for specific cost categories and RAG statuses across specified schools.")]
    [OpenApiUrnsParameter(Required = false)]
    [OpenApiCompanyNumberParameter]
    [OpenApiCategoriesParameter(Example = typeof(OpenApiExamples.CategoryCost))]
    [OpenApiStatusesParameter(Example = typeof(OpenApiExamples.RagStatuses))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(DetailResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "Validation errors or bad request.")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.Details)] HttpRequestData req,
        CancellationToken token = default)
    {
        var context = new BasicContext(req, token);
        return await RunAsync(context);
    }
}