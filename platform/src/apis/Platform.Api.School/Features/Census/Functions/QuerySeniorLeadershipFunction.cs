using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.School.Features.Census.Handlers;
using Platform.Api.School.Features.Census.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;

namespace Platform.Api.School.Features.Census.Functions;

public class QuerySeniorLeadershipFunction(IEnumerable<IQuerySeniorLeadershipHandler> handlers) : VersionedFunctionBase<IQuerySeniorLeadershipHandler, BasicContext>(handlers)
{
    [Function(nameof(QuerySeniorLeadershipFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(QuerySeniorLeadershipFunction), Constants.Features.Census, Summary = "Query senior leadership census data", Description = "Returns senior leadership workforce data for a specific collection of schools based on URNs.")]
    [OpenApiUrnsParameter(Required = true)]
    [OpenApiDimensionParameter(Required = false, Example = typeof(OpenApiExamples.SeniorLeadershipDimension))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(CensusResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "Validation errors or bad request.")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.SeniorLeadership)] HttpRequestData req,
        CancellationToken token = default)
    {
        var context = new BasicContext(req, token);
        return await RunAsync(context);
    }
}