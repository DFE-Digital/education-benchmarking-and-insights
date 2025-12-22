using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Handlers;

public interface IGetStatisticalNeighboursHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken);
}

public class GetStatisticalNeighboursV1Handler(IStatisticalNeighboursService service) : IGetStatisticalNeighboursHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken)
    {
        var data = await service.GetAsync(identifier, cancellationToken);
        var response = data.MapToApiResponse();

        return response == null
            ? request.CreateNotFoundResponse()
            : await request.CreateJsonResponseAsync(response, cancellationToken);
    }
}