using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.LocalAuthority.Features.Details.Handlers;
using Platform.Api.LocalAuthority.Features.Details.Models;
using Platform.Functions;
using Platform.OpenApi;
using Platform.OpenApi.Attributes;

namespace Platform.Api.LocalAuthority.Features.Details.Functions;

public class QueryLocalAuthoritiesFunction(IEnumerable<IQueryLocalAuthoritiesHandler> handlers) : VersionedFunctionBase<IQueryLocalAuthoritiesHandler, BasicContext>(handlers)
{
    [Function(nameof(QueryLocalAuthoritiesFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(QueryLocalAuthoritiesFunction), Constants.Features.Details, Summary = "Get all local authorities", Description = "Returns a list of all local authorities.")]
    [OpenApiApiVersionParameter]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(LocalAuthorityResponse[]), Description = "The collection of local authorities")]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails), Description = "The request was invalid")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.LocalAuthorityCollection)] HttpRequestData req,
        CancellationToken token = default)
    {
        var context = new BasicContext(req, token);
        return await RunAsync(context);
    }
}