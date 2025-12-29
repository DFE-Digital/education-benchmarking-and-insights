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
using Platform.Functions.OpenApi;

namespace Platform.Api.School.Features.Accounts.Functions;

public class GetBalanceFunction(IVersionedHandlerDispatcher<IGetBalanceHandler> dispatcher) : VersionedFunctionBase<IGetBalanceHandler>(dispatcher)
{
    [Function(nameof(GetBalanceFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetBalanceFunction), Constants.Features.Accounts)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(BalanceResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.Balance)] HttpRequestData req,
        string urn,
        CancellationToken token = default)
    {
        return await WithHandlerAsync(
            req,
            handler => handler.HandleAsync(req, urn, token),
            token);
    }
}