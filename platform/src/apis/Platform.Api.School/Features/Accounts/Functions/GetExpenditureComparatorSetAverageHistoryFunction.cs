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

public class GetExpenditureComparatorSetAverageHistoryFunction(IEnumerable<IGetExpenditureComparatorSetAverageHistoryHandler> handlers) : VersionedFunctionBase<IGetExpenditureComparatorSetAverageHistoryHandler, IdContext>(handlers)
{
    [Function(nameof(GetExpenditureComparatorSetAverageHistoryFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetExpenditureComparatorSetAverageHistoryFunction), Constants.Features.Accounts, Summary = "Get school expenditure comparator set average history", Description = "Returns historical average expenditure for a school's statistical comparator set.")]
    [OpenApiUrnParameter]
    [OpenApiDimensionParameter(Required = true, Example = typeof(OpenApiExamples.Dimension))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(ExpenditureHistoryResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "Validation errors or bad request.")]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.ExpenditureComparatorSetAverageHistory)] HttpRequestData req,
        string urn,
        CancellationToken token = default)
    {
        var context = new IdContext(req, token, urn);
        return await RunAsync(context);
    }
}