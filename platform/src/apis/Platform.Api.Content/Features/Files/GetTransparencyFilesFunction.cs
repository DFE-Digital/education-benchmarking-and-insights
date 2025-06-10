using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Content.Features.Files.Responses;
using Platform.Api.Content.Features.Files.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Content.Features.Files;

public class GetTransparencyFilesFunction(IFilesService service)
{
    [Function(nameof(GetTransparencyFilesFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetTransparencyFilesFunction), Constants.Features.Files)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(FileResponse[]))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.Transparency)] HttpRequestData req,
        CancellationToken cancellationToken = default)
    {
        var result = await service.GetActiveFilesByType(cancellationToken, "transparency-aar", "transparency-cfr");
        return await req.CreateJsonResponseAsync(result.MapToApiResponse(), cancellationToken);
    }
}