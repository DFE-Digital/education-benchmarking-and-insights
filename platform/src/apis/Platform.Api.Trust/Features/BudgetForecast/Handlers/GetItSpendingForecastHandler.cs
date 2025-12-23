using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Platform.Api.Trust.Features.BudgetForecast.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Trust.Features.BudgetForecast.Handlers;

public interface IGetItSpendingForecastHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken);
}

public class GetItSpendingForecastV1Handler(IBudgetForecastService service) : IGetItSpendingForecastHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken)
    {
        var result = await service.GetItSpendingForecastAsync(identifier, cancellationToken);

        return result.IsEmpty()
            ? request.CreateNotFoundResponse()
            : await request.CreateJsonResponseAsync(result, cancellationToken);
    }
}