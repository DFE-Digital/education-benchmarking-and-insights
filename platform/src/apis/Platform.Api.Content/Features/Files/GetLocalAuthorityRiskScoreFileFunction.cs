using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Content.Features.Files.Handlers;
using Platform.Api.Content.Features.Files.Responses;
using Platform.Functions;
using Platform.OpenApi;

namespace Platform.Api.Content.Features.Files;

public class GetLocalAuthorityRiskScoreFileFunction(IEnumerable<IGetLocalAuthorityRiskScoreFileHandler> handlers)
    : VersionedFunctionBase<IGetLocalAuthorityRiskScoreFileHandler, IdContext>(handlers)
{
    [Function(nameof(GetLocalAuthorityRiskScoreFileFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetLocalAuthorityRiskScoreFileFunction), Constants.Features.Files)]
    [OpenApiParameter("year", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The year identifier")]
    [OpenApiParameter(Domain.Constants.ApiVersion, Type = typeof(string), In = ParameterLocation.Header)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(FileResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.NotFound, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.LocalAuthorityRiskScores)] HttpRequestData req,
        string year,
        CancellationToken token = default)
    {
        var context = new IdContext(req, token, year);
        return await RunAsync(context);
    }
}
