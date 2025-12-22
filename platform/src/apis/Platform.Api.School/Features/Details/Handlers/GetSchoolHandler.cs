using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Details.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Details.Handlers;

public interface IGetSchoolHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken);
}

public class GetSchoolV1Handler(ISchoolDetailsService service) : IGetSchoolHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken)
    {
        var school = await service.GetAsync(identifier, cancellationToken);

        return school == null
            ? request.CreateNotFoundResponse()
            : await request.CreateJsonResponseAsync(school, cancellationToken);
    }
}