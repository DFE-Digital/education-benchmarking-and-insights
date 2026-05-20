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

public class GetExpenditureFunction(IEnumerable<IGetExpenditureHandler> handlers) : VersionedFunctionBase<IGetExpenditureHandler, IdContext>(handlers)
{
    [Function(nameof(GetExpenditureFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetExpenditureFunction), Constants.Features.Accounts, Summary = "Get school expenditure data", Description = "Returns detailed expenditure data for a specific school, optionally filtered by category.")]
    [OpenApiUrnParameter]
    [OpenApiCategoryParameter(Example = typeof(OpenApiExamples.Category))]
    [OpenApiDimensionParameter(Example = typeof(OpenApiExamples.Dimension))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(ExpenditureResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "Validation errors or bad request.")]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.ExpenditureSingle)] HttpRequestData req,
        string urn,
        CancellationToken token = default)
    {
        var context = new IdContext(req, token, urn);
        return await RunAsync(context);
    }
}
