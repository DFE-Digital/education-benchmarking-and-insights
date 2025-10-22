using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.Comparators.Models;
using Platform.Api.Trust.Features.Comparators.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Trust.Features.Comparators.Handlers;

public interface IPostComparatorsHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken);
}

public class PostComparatorsV1Handler(IComparatorsService service) : IPostComparatorsHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken)
    {
        var body = await request.ReadAsJsonAsync<ComparatorsRequest>(cancellationToken);
        var comparators = await service.ComparatorsAsync(identifier, body, cancellationToken);
        return await request.CreateJsonResponseAsync(comparators, cancellationToken);
    }
}