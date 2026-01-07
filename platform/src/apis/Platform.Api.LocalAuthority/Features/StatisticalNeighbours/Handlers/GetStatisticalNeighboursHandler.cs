using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Handlers;

public interface IGetStatisticalNeighboursHandler : IVersionedHandler<IdContext>;

public class GetStatisticalNeighboursV1Handler(IStatisticalNeighboursService service) : IGetStatisticalNeighboursHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        var data = await service.GetAsync(context.Id, context.Token);
        var response = data.MapToApiResponse();

        return response == null
            ? context.Request.CreateNotFoundResponse()
            : await context.Request.CreateJsonResponseAsync(response, context.Token);
    }
}