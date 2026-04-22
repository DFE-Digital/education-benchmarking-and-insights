using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.LocalAuthority.Features.Accounts.Handlers;
using Platform.Api.LocalAuthority.Features.Accounts.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;
using Platform.Functions.OpenApi.Attributes;

namespace Platform.Api.LocalAuthority.Features.Accounts.Functions;

public class QueryHighNeedsFunction(IEnumerable<IQueryHighNeedsHandler> handlers) : VersionedFunctionBase<IQueryHighNeedsHandler, BasicContext>(handlers)
{
    [Function(nameof(QueryHighNeedsFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(QueryHighNeedsFunction), Constants.Features.Accounts, Summary = "Get local authority high needs data", Description = "Returns the high needs data for the specified local authorities.")]
    [OpenApiApiVersionParameter]
    [OpenApiLaCodesParameter("code", isRequired: true)]
    [OpenApiDimensionParameter(Required = true, Example = typeof(OpenApiExamples.DimensionHighNeeds))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(LocalAuthority<HighNeedsYear>[]), Description = "The high needs data for the requested local authorities")]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "The request was invalid")]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound, Description = "The requested local authorities could not be found")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.HighNeeds)] HttpRequestData req,
        CancellationToken token = default)
    {
        var context = new BasicContext(req, token);
        return await RunAsync(context);
    }
}