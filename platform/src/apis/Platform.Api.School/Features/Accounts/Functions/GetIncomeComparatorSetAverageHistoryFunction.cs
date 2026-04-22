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
using Platform.Functions.OpenApi.Attributes;

namespace Platform.Api.School.Features.Accounts.Functions;

public class GetIncomeComparatorSetAverageHistoryFunction(IEnumerable<IGetIncomeComparatorSetAverageHistoryHandler> handlers) : VersionedFunctionBase<IGetIncomeComparatorSetAverageHistoryHandler, IdContext>(handlers)
{
    [Function(nameof(GetIncomeComparatorSetAverageHistoryFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetIncomeComparatorSetAverageHistoryFunction), Constants.Features.Accounts, Summary = "Get school income comparator set average history", Description = "Returns historical average income for a school's statistical comparator set.")]
    [OpenApiUrnParameter]
    [OpenApiDimensionParameter(Required = true, Example = typeof(OpenApiExamples.Dimension))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(IncomeHistoryResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "Validation errors or bad request.")]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.IncomeComparatorSetAverageHistory)] HttpRequestData req,
        string urn,
        CancellationToken token = default)
    {
        var context = new IdContext(req, token, urn);
        return await RunAsync(context);
    }
}