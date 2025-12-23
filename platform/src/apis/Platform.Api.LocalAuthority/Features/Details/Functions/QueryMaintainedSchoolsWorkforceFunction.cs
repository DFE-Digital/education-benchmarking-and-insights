using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.LocalAuthority.Features.Details.Handlers;
using Platform.Api.LocalAuthority.Features.Details.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;

namespace Platform.Api.LocalAuthority.Features.Details.Functions;

public class QueryMaintainedSchoolsWorkforceFunction(IVersionedHandlerDispatcher<IQueryMaintainedSchoolWorkforceHandler> dispatcher) : VersionedFunctionBase<IQueryMaintainedSchoolWorkforceHandler>(dispatcher)
{
    [Function(nameof(QueryMaintainedSchoolsWorkforceFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(QueryMaintainedSchoolsWorkforceFunction), Constants.Features.Details)]
    [OpenApiParameter(Platform.Functions.Constants.ApiVersion, Type = typeof(string), Required = false, In = ParameterLocation.Header)]
    [OpenApiParameter("code", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for resultant values", Type = typeof(string), Example = typeof(OpenApiExamples.DimensionWorkforce))]
    [OpenApiParameter("nurseryProvision", In = ParameterLocation.Query, Description = "List of nursery provisions to filter resultant values", Type = typeof(string[]), Required = false)]
    [OpenApiParameter("sixthFormProvision", In = ParameterLocation.Query, Description = "List of sixth provisions filter resultant values", Type = typeof(string[]), Required = false)]
    [OpenApiParameter("specialClassesProvision", In = ParameterLocation.Query, Description = "List of special class provisions filter resultant values", Type = typeof(string[]), Required = false)]
    [OpenApiParameter("overallPhase", In = ParameterLocation.Query, Description = "Phase to filter resultant values", Type = typeof(string[]), Required = false)]
    [OpenApiParameter("sortField", In = ParameterLocation.Query, Description = "Field to sort by", Type = typeof(string), Required = false)]
    [OpenApiParameter("sortOrder", In = ParameterLocation.Query, Description = "Sort direction: 'asc' or 'desc'", Type = typeof(string), Required = false)]
    [OpenApiParameter("limit", In = ParameterLocation.Query, Description = "Number of records to return if empty all are returned", Type = typeof(string), Required = false)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(LocalAuthoritySchoolWorkforceSummaryResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "Validation errors or bad request.")]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.MaintainedSchoolsWorkforce)] HttpRequestData req,
        string code,
        CancellationToken token = default)
    {
        return await WithHandlerAsync(
            req,
            handler => handler.HandleAsync(req, code, token),
            token);
    }
}