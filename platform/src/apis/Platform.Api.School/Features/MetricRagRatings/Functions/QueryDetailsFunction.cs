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

public class QueryDetailsFunction(IVersionedHandlerDispatcher<IQueryDetailsHandler> dispatcher) : VersionedFunctionBase<IQueryDetailsHandler>(dispatcher)
{
    [Function(nameof(QueryDetailsFunction))]
    [OpenApiOperation(nameof(QueryDetailsFunction), Constants.Features.MetricRagRatings)]
    [OpenApiParameter("urns", In = ParameterLocation.Query, Description = "List of school URNs", Type = typeof(string[]), Required = false)]
    [OpenApiParameter("companyNumber", In = ParameterLocation.Query, Description = "Eight digit trust company number", Type = typeof(string), Required = false)]
    [OpenApiParameter("categories", In = ParameterLocation.Query, Description = "List of cost category", Type = typeof(string[]), Example = typeof(OpenApiExamples.CategoryCost), Required = false)]
    [OpenApiParameter("statuses", In = ParameterLocation.Query, Description = "List of RAG statuses", Type = typeof(string[]), Example = typeof(OpenApiExamples.RagStatuses), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(DetailResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "Validation errors or bad request.")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.Details)] HttpRequestData req,
        CancellationToken token = default)
    {
        return await WithHandlerAsync(
            req,
            handler => handler.HandleAsync(req, token),
            token);
    }
}