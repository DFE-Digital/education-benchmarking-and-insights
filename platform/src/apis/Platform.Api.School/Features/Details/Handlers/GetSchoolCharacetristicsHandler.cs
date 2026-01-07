using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Details.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Details.Handlers;

public interface IGetSchoolCharacteristicsHandler : IVersionedHandler<IdContext>;

public class GetSchoolCharacteristicsV1Handler(ISchoolDetailsService service) : IGetSchoolCharacteristicsHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        var result = await service.GetCharacteristicAsync(context.Id, context.Token);
        return result == null
            ? context.Request.CreateNotFoundResponse()
            : await context.Request.CreateJsonResponseAsync(result, context.Token);
    }
}