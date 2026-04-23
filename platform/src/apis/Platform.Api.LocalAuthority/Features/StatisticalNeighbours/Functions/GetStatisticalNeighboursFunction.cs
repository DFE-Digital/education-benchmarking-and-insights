using System.Collections.Generic;
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
using Platform.OpenApi;
using Platform.OpenApi.Attributes;

namespace Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Functions;

public class GetStatisticalNeighboursFunction(IEnumerable<IGetStatisticalNeighboursHandler> handlers) : VersionedFunctionBase<IGetStatisticalNeighboursHandler, IdContext>(handlers)
{
    [Function(nameof(GetStatisticalNeighboursFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetStatisticalNeighboursFunction), Constants.Features.StatisticalNeighbours, Summary = "Get statistical neighbours", Description = "Returns a list of statistical neighbours for the specified local authority.")]
    [OpenApiLaCodeParameter("code", ParameterLocation.Path)]
    [OpenApiApiVersionParameter]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(StatisticalNeighboursResponse), Description = "The statistical neighbours for the requested local authority")]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails), Description = "The request was invalid")]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound, Description = "The requested local authority could not be found")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.StatisticalNeighbours)] HttpRequestData req,
        string code,
        CancellationToken token = default)
    {
        var context = new IdContext(req, token, code);
        return await RunAsync(context);
    }
}
