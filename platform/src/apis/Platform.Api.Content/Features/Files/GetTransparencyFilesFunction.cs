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
using Platform.Functions.OpenApi;

namespace Platform.Api.Content.Features.Files;

public class GetTransparencyFilesFunction(IEnumerable<IGetTransparencyFilesHandler> handlers) : VersionedFunctionBase<IGetTransparencyFilesHandler, BasicContext>(handlers)
{
    [Function(nameof(GetTransparencyFilesFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetTransparencyFilesFunction), Constants.Features.Files)]
    [OpenApiParameter(Functions.Constants.ApiVersion, Type = typeof(string), In = ParameterLocation.Header)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(FileResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.Transparency)] HttpRequestData req,
        CancellationToken token = default)
    {
        var context = new BasicContext(req, token);
        return await RunAsync(context);
    }
}