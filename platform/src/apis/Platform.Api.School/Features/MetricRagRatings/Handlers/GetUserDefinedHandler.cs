using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.MetricRagRatings.Parameters;
using Platform.Api.School.Features.MetricRagRatings.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.MetricRagRatings.Handlers;

public interface IGetUserDefinedHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken);
}

public class GetUserDefinedV1Handler(IMetricRagRatingsService service) : IGetUserDefinedHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken)
    {
        var queryParams = request.GetParameters<UserDefinedParameters>();

        var result = await service.GetUserDefinedAsync(identifier, queryParams.DataContext, cancellationToken: cancellationToken);
        return await request.CreateJsonResponseAsync(result, cancellationToken);
    }
}