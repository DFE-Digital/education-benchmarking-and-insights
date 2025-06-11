using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Content.Features.Years.Handlers;
using Platform.Api.Content.Features.Years.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Content.Features.Years;

public class GetCurrentReturnYearsFunction(IVersionedHandlerDispatcher<IGetCurrentReturnYearsHandler> dispatcher) : VersionedFunctionBase<IGetCurrentReturnYearsHandler>(dispatcher)
{
    [Function(nameof(GetCurrentReturnYearsFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetCurrentReturnYearsFunction), Constants.Features.Years)]
    [OpenApiParameter(Functions.Constants.ApiVersion, Type = typeof(string), In = ParameterLocation.Header)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(FinanceYears))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.CurrentReturn)] HttpRequestData req,
        CancellationToken cancellationToken = default)
    {
        return await WithHandlerAsync(
            req,
            handler => handler.HandleAsync(req, cancellationToken),
            cancellationToken);
    }
}