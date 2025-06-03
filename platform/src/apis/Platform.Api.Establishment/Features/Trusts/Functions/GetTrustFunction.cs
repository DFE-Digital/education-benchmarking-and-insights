using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Establishment.Features.Trusts.Handlers;
using Platform.Api.Establishment.Features.Trusts.Models;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Establishment.Features.Trusts.Functions;

public class GetTrustFunction(IVersionedHandlerDispatcher<IGetTrustHandler> dispatcher)
{
    [Function(nameof(GetTrustFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetTrustFunction), Constants.Features.Trusts)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(Trust))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.Trust)] HttpRequestData req,
        string identifier,
        CancellationToken cancellationToken = default)
    {
        var version = req.ReadVersion();
        var handler = dispatcher.GetHandler(version);

        return handler == null
            ? await req.CreateUnsupportedVersionResponseAsync(cancellationToken)
            : await handler.HandleAsync(req, identifier, cancellationToken);
    }
}