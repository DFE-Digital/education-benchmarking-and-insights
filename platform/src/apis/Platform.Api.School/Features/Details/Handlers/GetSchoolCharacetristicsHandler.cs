using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Details.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Details.Handlers;

public interface IGetSchoolCharacteristicsHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken);
}

public class GetSchoolCharacteristicsV1Handler(ISchoolDetailsService service) : IGetSchoolCharacteristicsHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken)
    {
        var result = await service.GetCharacteristicAsync(identifier, cancellationToken);
        return result == null
            ? request.CreateNotFoundResponse()
            : await request.CreateJsonResponseAsync(result, cancellationToken);
    }
}