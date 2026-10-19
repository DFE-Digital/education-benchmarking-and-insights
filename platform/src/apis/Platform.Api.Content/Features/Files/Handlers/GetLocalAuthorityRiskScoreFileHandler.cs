using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Content.Features.Files.Responses;
using Platform.Api.Content.Features.Files.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Content.Features.Files.Handlers;

public interface IGetLocalAuthorityRiskScoreFileHandler : IVersionedHandler<IdContext>;

public class GetLocalAuthorityRiskScoreFileV1Handler(IFilesService service) : IGetLocalAuthorityRiskScoreFileHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        var result = await service.GetLocalAuthorityRiskScoreFile(context.Id, context.Token);

        if (result is null)
        {
            return context.Request.CreateNotFoundResponse();
        }

        return await context.Request.CreateJsonResponseAsync(result.MapToApiResponse(), context.Token);
    }
}
