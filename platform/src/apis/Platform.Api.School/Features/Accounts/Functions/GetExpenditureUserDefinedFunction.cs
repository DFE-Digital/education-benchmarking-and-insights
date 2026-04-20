using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.School.Features.Accounts.Handlers;
using Platform.Api.School.Features.Accounts.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;

namespace Platform.Api.School.Features.Accounts.Functions;

public class GetExpenditureUserDefinedFunction(IEnumerable<IGetExpenditureUserDefinedHandler> handlers) : VersionedFunctionBase<IGetExpenditureUserDefinedHandler, IdPairContext>(handlers)
{
    [Function(nameof(GetExpenditureUserDefinedFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetExpenditureUserDefinedFunction), Constants.Features.Accounts, Summary = "Get expenditure for user-defined comparator set", Description = "Returns average expenditure for a user-defined set of schools (custom comparator set).")]
    [OpenApiUrnParameter]
    [OpenApiIdentifierParameter]
    [OpenApiCategoryParameter(Example = typeof(OpenApiExamples.Category))]
    [OpenApiDimensionParameter(Example = typeof(OpenApiExamples.Dimension))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(ExpenditureResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "Validation errors or bad request.")]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.ExpenditureUserDefined)] HttpRequestData req,
        string urn,
        string identifier,
        CancellationToken token = default)
    {
        var context = new IdPairContext(req, token, urn, identifier);
        return await RunAsync(context);
    }
}
