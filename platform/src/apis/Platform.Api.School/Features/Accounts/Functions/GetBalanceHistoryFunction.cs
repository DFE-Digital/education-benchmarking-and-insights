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

public class GetBalanceHistoryFunction(IEnumerable<IGetBalanceHistoryHandler> handlers) : VersionedFunctionBase<IGetBalanceHistoryHandler, IdContext>(handlers)
{
    //TODO: Consider adding validation for parameters
    [Function(nameof(GetBalanceHistoryFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetBalanceHistoryFunction), Constants.Features.Accounts, Summary = "Get school balance history", Description = "Returns a historical time-series of in-year and revenue reserve balances for a specific school.")]
    [OpenApiUrnParameter]
    [OpenApiDimensionParameter(Example = typeof(OpenApiExamples.Dimension))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(BalanceHistoryResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.BalanceHistory)] HttpRequestData req,
        string urn,
        CancellationToken token = default)
    {
        var context = new IdContext(req, token, urn);
        return await RunAsync(context);
    }
}