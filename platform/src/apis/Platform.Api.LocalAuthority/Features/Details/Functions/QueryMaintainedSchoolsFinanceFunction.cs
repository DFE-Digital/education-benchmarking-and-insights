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

public class QueryMaintainedSchoolsFinanceFunction(IEnumerable<IQueryMaintainedSchoolFinanceHandler> handlers) : VersionedFunctionBase<IQueryMaintainedSchoolFinanceHandler, IdContext>(handlers)
{
    [Function(nameof(QueryMaintainedSchoolsFinanceFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(QueryMaintainedSchoolsFinanceFunction), Constants.Features.Details, Summary = "Get maintained schools finance summary", Description = "Returns a finance summary for maintained schools within a specified local authority.")]
    [OpenApiApiVersionParameter]
    [OpenApiLaCodeParameter("code", ParameterLocation.Path)]
    [OpenApiDimensionParameter(Example = typeof(OpenApiExamples.DimensionFinance))]
    [OpenApiNurseryProvisionParameter]
    [OpenApiSixthFormProvisionParameter]
    [OpenApiSpecialClassesProvisionParameter]
    [OpenApiOverallPhaseParameter]
    [OpenApiSortFieldParameter]
    [OpenApiSortOrderParameter]
    [OpenApiLimitParameter]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(LocalAuthoritySchoolFinanceSummaryResponse[]), Description = "The finance summary for maintained schools in the requested local authority")]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails), Description = "The request was invalid")]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound, Description = "The requested local authority could not be found")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.MaintainedSchoolsFinance)] HttpRequestData req,
        string code,
        CancellationToken token = default)
    {
        var context = new IdContext(req, token, code);
        return await RunAsync(context);
    }
}