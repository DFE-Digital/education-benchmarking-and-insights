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

public class GetIncomeNationalAverageHistoryFunction(IEnumerable<IGetIncomeNationalAverageHistoryHandler> handlers) : VersionedFunctionBase<IGetIncomeNationalAverageHistoryHandler, BasicContext>(handlers)
{
    [Function(nameof(GetIncomeNationalAverageHistoryFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetIncomeNationalAverageHistoryFunction), Constants.Features.Accounts)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Required = true, Example = typeof(OpenApiExamples.Dimension))]
    [OpenApiParameter("phase", In = ParameterLocation.Query, Description = "Overall phase for response values", Type = typeof(string), Required = true, Example = typeof(OpenApiExamples.Phase))]
    [OpenApiParameter("financeType", In = ParameterLocation.Query, Description = "Finance type for response values", Type = typeof(string), Required = true, Example = typeof(OpenApiExamples.FinanceTypes))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(IncomeHistoryResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "Validation errors or bad request.")]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.IncomeNationalAverageHistory)] HttpRequestData req,
        CancellationToken token = default)
    {
        var context = new BasicContext(req, token);
        return await RunAsync(context);
    }
}