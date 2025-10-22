using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Trust.Features.Accounts.Handlers;
using Platform.Api.Trust.Features.Accounts.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Trust.Features.Accounts.Functions;

public class GetExpenditureHistoryFunction(IVersionedHandlerDispatcher<IGetExpenditureHistoryHandler> dispatcher) : VersionedFunctionBase<IGetExpenditureHistoryHandler>(dispatcher)
{
    [Function(nameof(GetExpenditureHistoryFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetExpenditureHistoryFunction), Constants.Features.Accounts)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter(Platform.Functions.Constants.ApiVersion, Type = typeof(string), Required = false, In = ParameterLocation.Header)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Required = true, Example = typeof(OpenApiExamples.DimensionFinance))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(ExpenditureHistoryResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "Validation errors or bad request.")]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.ExpenditureHistory)] HttpRequestData req,
        string companyNumber,
        CancellationToken token = default)
    {
        return await WithHandlerAsync(
            req,
            handler => handler.HandleAsync(req, companyNumber, token),
            token);
    }
}