using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Risks.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Risks.Handlers;

public interface IGetSchoolRisksHistoryHandler : IVersionedHandler<IdContext>;

public class GetSchoolRisksHistoryHandlerV1(ISchoolRisksService service) : IGetSchoolRisksHistoryHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        var (years, rows) = await service.GetHistoryAsync(context.Id, context.Token);
        return years == null
            ? context.Request.CreateNotFoundResponse()
            : await context.Request.CreateJsonResponseAsync(years.MapToApiResponse(rows), context.Token);
    }
}
