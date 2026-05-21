using System.Collections.Generic;
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
using Platform.OpenApi;
using Platform.OpenApi.Attributes;

namespace Platform.Api.School.Features.Census.Functions;

public class GetCensusUserDefinedFunction(IEnumerable<IGetUserDefinedHandler> handlers) : VersionedFunctionBase<IGetUserDefinedHandler, IdPairContext>(handlers)
{
    [Function(nameof(GetCensusUserDefinedFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetCensusUserDefinedFunction), Constants.Features.Census, Summary = "Get census data for a user-defined comparator set", Description = "Returns average census and workforce data for a user-defined custom comparator set.")]
    [OpenApiUrnParameter]
    [OpenApiIdentifierParameter]
    [OpenApiCategoryParameter(Required = false, Example = typeof(OpenApiExamples.Category))]
    [OpenApiDimensionParameter(Example = typeof(OpenApiExamples.Dimension))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(CensusResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "Validation errors or bad request.")]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.UserDefined)] HttpRequestData req,
        string urn,
        string identifier,
        CancellationToken token = default)
    {
        var context = new IdPairContext(req, token, urn, identifier);
        return await RunAsync(context);
    }
}
