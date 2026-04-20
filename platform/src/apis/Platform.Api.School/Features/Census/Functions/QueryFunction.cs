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

public class QueryFunction(IEnumerable<IQueryHandler> handlers) : VersionedFunctionBase<IQueryHandler, BasicContext>(handlers)
{
    //TODO: Consider separate end points for Trust and LA (i.e. census/trust/{id}/schools)
    [Function(nameof(QueryFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(QueryFunction), Constants.Features.Census, Summary = "Query census data across multiple schools", Description = "Returns census and workforce data for a collection of schools based on URNs, Local Authority code, or Company Number.")]
    [OpenApiUrnsParameter]
    [OpenApiPhaseParameter(Example = typeof(OpenApiExamples.Phase))]
    [OpenApiCompanyNumberParameter]
    [OpenApiLaCodeParameter]
    [OpenApiCategoryParameter(Required = true, Example = typeof(OpenApiExamples.Category))]
    [OpenApiDimensionParameter(Required = true, Example = typeof(OpenApiExamples.Dimension))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(CensusResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "Validation errors or bad request.")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.Collection)] HttpRequestData req,
        CancellationToken token = default)
    {
        var context = new BasicContext(req, token);
        return await RunAsync(context);
    }
}
