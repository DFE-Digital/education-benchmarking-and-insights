using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Details.Parameters;
using Platform.Api.School.Features.Details.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Details.Handlers;

public interface IQuerySchoolsHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken);
}

public class QuerySchoolsV1Handler(ISchoolDetailsService service) : IQuerySchoolsHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken)
    {
        var queryParams = request.GetParameters<SchoolsParameters>();

        var schools = await service.QueryAsync(queryParams.Schools, cancellationToken);
        return await request.CreateJsonResponseAsync(schools, cancellationToken);
    }
}