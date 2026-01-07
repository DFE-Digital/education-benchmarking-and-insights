using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Content.Features.Files.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Content.Features.Files.Handlers;

public interface IGetTransparencyFilesHandler : IVersionedHandler<BasicContext>;

public class GetTransparencyFilesV1Handler(IFilesService service) : IGetTransparencyFilesHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(BasicContext context)
    {
        var result = await service.GetActiveFilesByType(context.Token, "transparency-aar", "transparency-cfr");
        return await context.Request.CreateJsonResponseAsync(result.MapToApiResponse(), context.Token);
    }
}