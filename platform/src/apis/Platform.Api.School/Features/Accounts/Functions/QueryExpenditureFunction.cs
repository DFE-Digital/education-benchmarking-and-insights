using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.School.Features.Accounts.Handlers;
using Platform.Api.School.Features.Accounts.Models;
using Platform.Functions;
using Platform.OpenApi;
using Platform.OpenApi.Attributes;

namespace Platform.Api.School.Features.Accounts.Functions;

public class QueryExpenditureFunction(IEnumerable<IQueryExpenditureHandler> handlers) : VersionedFunctionBase<IQueryExpenditureHandler, BasicContext>(handlers)
{
    [Function(nameof(QueryExpenditureFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(QueryExpenditureFunction), Constants.Features.Accounts, Summary = "Query expenditure across multiple schools", Description = "Returns expenditure data for a collection of schools based on URNs, Local Authority code, or Company Number.")]
    [OpenApiUrnsParameter]
    [OpenApiPhaseParameter(Example = typeof(OpenApiExamples.Phase))]
    [OpenApiCompanyNumberParameter]
    [OpenApiLaCodeParameter]
    [OpenApiCategoryParameter(Example = typeof(OpenApiExamples.Category))]
    [OpenApiDimensionParameter(Example = typeof(OpenApiExamples.Dimension))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(ExpenditureResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "Validation errors or bad request.")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.ExpenditureCollection)] HttpRequestData req,
        CancellationToken token = default)
    {
        var context = new BasicContext(req, token);
        return await RunAsync(context);
    }
}
