using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Trust.Features.Details.Handlers;
using Platform.Api.Trust.Features.Details.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Trust.Features.Details.Functions;

public class GetTrustFunction(IEnumerable<IGetTrustHandler> handlers) : VersionedFunctionBase<IGetTrustHandler, IdContext>(handlers)
{
    [Function(nameof(GetTrustFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetTrustFunction), Constants.Features.Details)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter(Platform.Functions.Constants.ApiVersion, Type = typeof(string), Required = false, In = ParameterLocation.Header)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(TrustResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.TrustSingle)] HttpRequestData req,
        string companyNumber,
        CancellationToken token = default)
    {
        var context = new IdContext(req, token, companyNumber);
        return await RunAsync(context);
    }
}