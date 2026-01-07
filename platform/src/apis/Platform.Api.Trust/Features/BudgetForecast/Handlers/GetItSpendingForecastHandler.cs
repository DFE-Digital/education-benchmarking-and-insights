using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Platform.Api.Trust.Features.BudgetForecast.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Trust.Features.BudgetForecast.Handlers;

public interface IGetItSpendingForecastHandler : IVersionedHandler<IdContext>;

public class GetItSpendingForecastV1Handler(IBudgetForecastService service) : IGetItSpendingForecastHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        var result = await service.GetItSpendingForecastAsync(context.Id, context.Token);

        return result.IsEmpty()
            ? context.Request.CreateNotFoundResponse()
            : await context.Request.CreateJsonResponseAsync(result, context.Token);
    }
}