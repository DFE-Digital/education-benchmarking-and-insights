using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Content.Features.Years.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Content.Features.Years.Handlers;

public interface IGetCurrentReturnYearsHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken);
}

public class GetCurrentReturnYearsV1Handler(IYearsService service) : IGetCurrentReturnYearsHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken)
    {
        var result = await service.GetCurrentReturnYears(cancellationToken);
        return await request.CreateJsonResponseAsync(result, cancellationToken);
    }
}