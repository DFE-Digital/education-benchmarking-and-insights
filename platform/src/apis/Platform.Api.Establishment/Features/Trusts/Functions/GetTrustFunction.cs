using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Establishment.Features.Trusts.Handlers;
using Platform.Api.Establishment.Features.Trusts.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Establishment.Features.Trusts.Functions;

public class GetTrustFunction(IVersionedHandlerDispatcher<IGetTrustHandler> dispatcher) : VersionedFunctionBase<IGetTrustHandler>(dispatcher)
{
    [Function(nameof(GetTrustFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetTrustFunction), Constants.Features.Trusts)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiParameter(Platform.Functions.Constants.ApiVersion, Type = typeof(string), Required = false, In = ParameterLocation.Header)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(Trust))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.Trust)] HttpRequestData req,
        string identifier,
        CancellationToken token = default)
    {
        return await WithHandlerAsync(
            req,
            handler => handler.HandleAsync(req, identifier, token),
            token);
    }
}