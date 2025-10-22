using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.BudgetForecast.Services;
using Platform.Domain;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Trust.Features.BudgetForecast.Handlers;

public interface IGetForecastRiskMetricsHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken);
}

public class GetForecastRiskMetricsV1Handler(IBudgetForecastService service) : IGetForecastRiskMetricsHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken)
    {
        var result = await service.GetBudgetForecastReturnMetricsAsync(identifier, Pipeline.RunType.Default, cancellationToken);
        return await request.CreateJsonResponseAsync(result.Select(Mapper.MapToApiResponse), cancellationToken);
    }
}