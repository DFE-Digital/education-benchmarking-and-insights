using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.BudgetForecast.Services;
using Platform.Domain;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Trust.Features.BudgetForecast.Handlers;

public interface IGetForecastRiskMetricsHandler : IVersionedHandler<IdContext>;

public class GetForecastRiskMetricsV1Handler(IBudgetForecastService service) : IGetForecastRiskMetricsHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        var result = await service.GetBudgetForecastReturnMetricsAsync(context.Id, Pipeline.RunType.Default, context.Token);
        return await context.Request.CreateJsonResponseAsync(result.Select(Mapper.MapToApiResponse), context.Token);
    }
}