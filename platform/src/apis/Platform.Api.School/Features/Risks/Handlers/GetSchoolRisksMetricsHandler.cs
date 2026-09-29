using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Risks.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Risks.Handlers;

public interface IGetSchoolRisksMetricsHandler : IVersionedHandler<IdContext>;

public class GetSchoolRisksMetricsHandlerV1(ISchoolRisksService service) : IGetSchoolRisksMetricsHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        var result = await service.GetMetricsAsync(context.Id, context.Token);
        return result.Any()
            ? await context.Request.CreateJsonResponseAsync(result, context.Token)
            : context.Request.CreateNotFoundResponse();
    }
}
