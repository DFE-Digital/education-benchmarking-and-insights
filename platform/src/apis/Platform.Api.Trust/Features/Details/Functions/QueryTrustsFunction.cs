using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Trust.Features.Details.Handlers;
using Platform.Api.Trust.Features.Details.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Trust.Features.Details.Functions;

public class QueryTrustsFunction(IVersionedHandlerDispatcher<IQueryTrustsHandler> dispatcher) : VersionedFunctionBase<IQueryTrustsHandler>(dispatcher)
{
    [Function(nameof(QueryTrustsFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(QueryTrustsFunction), Constants.Features.Details)]
    [OpenApiParameter(Platform.Functions.Constants.ApiVersion, Type = typeof(string), Required = false, In = ParameterLocation.Header)]
    [OpenApiParameter("companyNumbers", In = ParameterLocation.Query, Description = "List of trust company numbers", Type = typeof(string[]), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(TrustCharacteristicResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.TrustCollection)] HttpRequestData req,
        CancellationToken token = default)
    {
        return await WithHandlerAsync(
            req,
            handler => handler.HandleAsync(req, token),
            token);
    }
}