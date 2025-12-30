using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.School.Features.Census.Handlers;
using Platform.Api.School.Features.Census.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;

namespace Platform.Api.School.Features.Census.Functions;

public class GetFunction(IVersionedHandlerDispatcher<IGetHandler> dispatcher) : VersionedFunctionBase<IGetHandler>(dispatcher)
{
    [Function(nameof(GetFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetFunction), Constants.Features.Census)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(CensusResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.Single)] HttpRequestData req,
        string urn,
        CancellationToken token = default)
    {
        return await WithHandlerAsync(
            req,
            handler => handler.HandleAsync(req, urn, token),
            token);
    }
}