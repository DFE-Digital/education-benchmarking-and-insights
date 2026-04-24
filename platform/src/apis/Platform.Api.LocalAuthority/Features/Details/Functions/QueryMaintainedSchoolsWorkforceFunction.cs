using System.Collections.Generic;
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
using Platform.OpenApi;
using Platform.OpenApi.Attributes;

namespace Platform.Api.LocalAuthority.Features.Details.Functions;

public class QueryMaintainedSchoolsWorkforceFunction(IEnumerable<IQueryMaintainedSchoolWorkforceHandler> handlers) : VersionedFunctionBase<IQueryMaintainedSchoolWorkforceHandler, IdContext>(handlers)
{
    [Function(nameof(QueryMaintainedSchoolsWorkforceFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(QueryMaintainedSchoolsWorkforceFunction), Constants.Features.Details, Summary = "Get maintained schools workforce summary", Description = "Returns a workforce summary for maintained schools within a specified local authority.")]
    [OpenApiApiVersionParameter]
    [OpenApiLaCodeParameter("code", ParameterLocation.Path)]
    [OpenApiDimensionParameter(Example = typeof(OpenApiExamples.DimensionWorkforce))]
    [OpenApiNurseryProvisionParameter]
    [OpenApiSixthFormProvisionParameter]
    [OpenApiSpecialClassesProvisionParameter]
    [OpenApiOverallPhaseParameter]
    [OpenApiSortFieldParameter]
    [OpenApiSortOrderParameter]
    [OpenApiLimitParameter]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(LocalAuthoritySchoolWorkforceSummaryResponse[]), Description = "The workforce summary for maintained schools in the requested local authority")]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "The request was invalid")]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound, Description = "The requested local authority could not be found")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.MaintainedSchoolsWorkforce)] HttpRequestData req,
        string code,
        CancellationToken token = default)
    {
        var context = new IdContext(req, token, code);
        return await RunAsync(context);
    }
}