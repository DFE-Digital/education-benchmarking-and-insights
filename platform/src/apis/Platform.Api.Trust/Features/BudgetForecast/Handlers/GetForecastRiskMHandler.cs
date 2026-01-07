using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.BudgetForecast.Parameters;
using Platform.Api.Trust.Features.BudgetForecast.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Trust.Features.BudgetForecast.Handlers;

public interface IGetForecastRiskMHandler : IVersionedHandler<IdContext>;

public class GetForecastRiskV1Handler(IBudgetForecastService service) : IGetForecastRiskMHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        var queryParams = context.Request.GetParameters<ForecastRiskParameters>();

        var bfr = await service.GetBudgetForecastReturnsAsync(context.Id, queryParams.RunType, queryParams.Category, queryParams.RunId, context.Token);
        var ar = await service.GetActualReturnsAsync(context.Id, queryParams.Category, queryParams.RunId, context.Token);

        return await context.Request.CreateJsonResponseAsync(Mapper.MapToApiResponse(bfr, ar), context.Token);
    }
}