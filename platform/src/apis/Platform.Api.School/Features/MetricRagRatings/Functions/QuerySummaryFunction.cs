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

namespace Platform.Api.School.Features.MetricRagRatings.Functions;

public class QuerySummaryFunction(IVersionedHandlerDispatcher<IQuerySummaryHandler> dispatcher) : VersionedFunctionBase<IQuerySummaryHandler>(dispatcher)
{
    [Function(nameof(QuerySummaryFunction))]
    [OpenApiOperation(nameof(QuerySummaryFunction), Constants.Features.MetricRagRatings)]
    [OpenApiParameter("urns", In = ParameterLocation.Query, Description = "List of school URNs", Type = typeof(string[]), Required = false)]
    [OpenApiParameter("companyNumber", In = ParameterLocation.Query, Description = "Eight digit trust company number", Type = typeof(string), Required = false)]
    [OpenApiParameter("laCode", In = ParameterLocation.Query, Description = "Three digit Local Authority code", Type = typeof(string), Required = false)]
    [OpenApiParameter("overallPhase", In = ParameterLocation.Query, Description = "School overall phase", Type = typeof(string), Required = false, Example = typeof(OpenApiExamples.Phase))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SummaryResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "Validation errors or bad request.")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.Summary)] HttpRequestData req,
        CancellationToken token = default)
    {
        return await WithHandlerAsync(
            req,
            handler => handler.HandleAsync(req, token),
            token);
    }
}