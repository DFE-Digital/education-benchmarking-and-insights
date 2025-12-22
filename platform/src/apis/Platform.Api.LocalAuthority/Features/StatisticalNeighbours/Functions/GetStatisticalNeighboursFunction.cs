using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Handlers;
using Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;

namespace Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Functions;

public class GetStatisticalNeighboursFunction(IVersionedHandlerDispatcher<IGetStatisticalNeighboursHandler> dispatcher) : VersionedFunctionBase<IGetStatisticalNeighboursHandler>(dispatcher)
{
    [Function(nameof(GetStatisticalNeighboursFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetStatisticalNeighboursFunction), Constants.Features.StatisticalNeighbours)]
    [OpenApiParameter("code", Type = typeof(string), Required = true)]
    [OpenApiParameter(Platform.Functions.Constants.ApiVersion, Type = typeof(string), Required = false, In = ParameterLocation.Header)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(StatisticalNeighboursResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.StatisticalNeighbours)] HttpRequestData req,
        string code,
        CancellationToken token = default)
    {
        return await WithHandlerAsync(
            req,
            handler => handler.HandleAsync(req, code, token),
            token);
    }
}