using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.LocalAuthority.Features.Risks.Handlers;
using Platform.Api.LocalAuthority.Features.Risks.Models;
using Platform.Functions;
using Platform.OpenApi;
using Platform.OpenApi.Attributes;

namespace Platform.Api.LocalAuthority.Features.Risks.Functions;

public class QueryPagedSchoolRisksFunction(IEnumerable<IQueryPagedSchoolRisksHandler> handlers) : VersionedFunctionBase<IQueryPagedSchoolRisksHandler, BasicContext>(handlers)
{
    [Function(nameof(QueryPagedSchoolRisksFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(QueryPagedSchoolRisksFunction), Constants.Features.Risks, Summary = "Get paged local authority schools risk data", Description = "Returns a paged list of school risk data for the specified local authorities.")]
    [OpenApiApiVersionParameter]
    [OpenApiLaCodesParameter("code", isRequired: true)]
    [OpenApiPageParameter(Required = false, Description = "Optional. The page number of the results to return. If omitted, will default to 1.")]
    [OpenApiPageSizeParameter(Required = false, Description = "Optional. The number of results to include in each page of a paginated response. If omitted, will default to 10.")]
    [OpenApiSortFieldParameter(Required = false, Example = typeof(OpenApiExamples.PagedSchoolRisksSortField), Description = "Optional. Field to sort by. If omitted, will be sorted by 'Overall'")]
    [OpenApiSortOrderParameter(Required = false, Example = typeof(OpenApiExamples.PagedSchoolRisksSortOrder), Description = "Optional. Sort direction: 'asc' or 'desc'. If omitted, will be sorted 'desc'.")]
    [OpenApiPhaseParameter(Required = false, Example = typeof(OpenApiExamples.PagedSchoolRisksPhase), Description = "Optional. Overall phase for response values. If omitted, all phases will be included.")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(PagedResult<SchoolRiskResponse>), Description = "The school risks data for the requested local authorities")]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "The request was invalid")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.PagedSchoolRisks)] HttpRequestData req,
        CancellationToken token = default)
    {
        var context = new BasicContext(req, token);
        return await RunAsync(context);
    }
}
