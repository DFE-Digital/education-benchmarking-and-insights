using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Trust.Features.Comparators.Handlers;
using Platform.Api.Trust.Features.Comparators.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Trust.Features.Comparators.Functions;

public class PostComparatorsFunction(IVersionedHandlerDispatcher<IPostComparatorsHandler> dispatcher) : VersionedFunctionBase<IPostComparatorsHandler>(dispatcher)
{
    //TODO : Consider request validation
    [Function(nameof(PostComparatorsFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(PostComparatorsFunction), Constants.Features.Comparators)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter(Platform.Functions.Constants.ApiVersion, Type = typeof(string), Required = false, In = ParameterLocation.Header)]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(ComparatorsRequest), Description = "The comparator characteristics object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(ComparatorsResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = Routes.Comparators)] HttpRequestData req,
        string companyNumber,
        CancellationToken token = default)
    {
        return await WithHandlerAsync(
            req,
            handler => handler.HandleAsync(req, companyNumber, token),
            token);
    }
}