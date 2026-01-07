using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Details.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Details.Handlers;

public interface IGetSchoolHandler : IVersionedHandler<IdContext>;

public class GetSchoolV1Handler(ISchoolDetailsService service) : IGetSchoolHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        var school = await service.GetAsync(context.Id, context.Token);

        return school == null
            ? context.Request.CreateNotFoundResponse()
            : await context.Request.CreateJsonResponseAsync(school, context.Token);
    }
}