using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Risks.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Risks.Handlers;

public interface IGetSchoolRisksHandler : IVersionedHandler<IdContext>;

public class GetSchoolRisksHandlerV1(ISchoolRisksService service) : IGetSchoolRisksHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        var result = await service.GetAsync(context.Id, context.Token);
        return result == null
            ? context.Request.CreateNotFoundResponse()
            : await context.Request.CreateJsonResponseAsync(result, context.Token);
    }
}
