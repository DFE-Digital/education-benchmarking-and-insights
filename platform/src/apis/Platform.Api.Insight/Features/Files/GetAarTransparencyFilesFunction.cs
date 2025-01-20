using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Insight.Features.Files.Responses;
using Platform.Api.Insight.Features.Files.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Insight.Features.Files;

public class GetAarTransparencyFilesFunction(IFilesService service)
{
    [Function(nameof(GetAarTransparencyFilesFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetAarTransparencyFilesFunction), Constants.Features.Files)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(FileResponse[]))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.TransparencyAar)] HttpRequestData req)
    {
        var result = await service.GetAarTransparencyFiles();
        return await req.CreateJsonResponseAsync(result.MapToApiResponse());
    }
}