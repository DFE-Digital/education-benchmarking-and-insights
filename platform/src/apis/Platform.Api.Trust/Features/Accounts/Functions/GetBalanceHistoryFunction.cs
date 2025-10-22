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

public class GetBalanceHistoryFunction(IVersionedHandlerDispatcher<IGetBalanceHistoryHandler> dispatcher) : VersionedFunctionBase<IGetBalanceHistoryHandler>(dispatcher)
{
    //TODO: Consider adding validation for parameters
    [Function(nameof(GetBalanceHistoryFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetBalanceHistoryFunction), Constants.Features.Accounts)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter(Platform.Functions.Constants.ApiVersion, Type = typeof(string), Required = false, In = ParameterLocation.Header)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(OpenApiExamples.DimensionFinance))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(BalanceHistoryResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.BalanceHistory)] HttpRequestData req,
        string companyNumber,
        CancellationToken token = default)
    {
        return await WithHandlerAsync(
            req,
            handler => handler.HandleAsync(req, companyNumber, token),
            token);
    }
}