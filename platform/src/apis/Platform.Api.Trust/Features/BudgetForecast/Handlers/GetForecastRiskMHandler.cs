using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.BudgetForecast.Parameters;
using Platform.Api.Trust.Features.BudgetForecast.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Trust.Features.BudgetForecast.Handlers;

public interface IGetForecastRiskMHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken);
}

public class GetForecastRiskV1Handler(IBudgetForecastService service) : IGetForecastRiskMHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken)
    {
        var queryParams = request.GetParameters<ForecastRiskParameters>();

        var bfr = await service.GetBudgetForecastReturnsAsync(identifier, queryParams.RunType, queryParams.Category, queryParams.RunId, cancellationToken);
        var ar = await service.GetActualReturnsAsync(identifier, queryParams.Category, queryParams.RunId, cancellationToken);

        return await request.CreateJsonResponseAsync(Mapper.MapToApiResponse(bfr, ar), cancellationToken);
    }
}