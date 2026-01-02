using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.School.Features.Census.Handlers;
using Platform.Api.School.Features.Census.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;

namespace Platform.Api.School.Features.Census.Functions;

public class GetCensusUserDefinedFunction(IVersionedHandlerDispatcher<IGetUserDefinedHandler> dispatcher) : VersionedFunctionBase<IGetUserDefinedHandler>(dispatcher)
{
    [Function(nameof(GetCensusUserDefinedFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetCensusUserDefinedFunction), Constants.Features.Census)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Census category", Type = typeof(string), Required = false, Example = typeof(OpenApiExamples.Category))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(OpenApiExamples.Dimension))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(CensusResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "Validation errors or bad request.")]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.UserDefined)] HttpRequestData req,
        string urn,
        string identifier,
        CancellationToken token = default)
    {
        return await WithHandlerAsync(
            req,
            handler => handler.HandleAsync(req, urn, identifier, token),
            token);
    }
}