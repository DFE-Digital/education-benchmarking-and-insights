using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Insight.Features.BudgetForecast.Responses;
using Platform.Api.Insight.Features.BudgetForecast.Services;
using Platform.Domain;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Insight.Features.BudgetForecast;

public class GetBudgetForecastMetricsFunction(IBudgetForecastService service)
{
    [Function(nameof(GetBudgetForecastMetricsFunction))]
    [OpenApiOperation(nameof(GetBudgetForecastMetricsFunction), Constants.Features.BudgetForecast)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(BudgetForecastReturnMetricResponse[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.Metrics)] HttpRequestData req,
        string companyNumber,
        CancellationToken cancellationToken = default)
    {
        var result = await service.GetBudgetForecastReturnMetricsAsync(companyNumber, Pipeline.RunType.Default, cancellationToken);
        return await req.CreateJsonResponseAsync(result.Select(Mapper.MapToApiResponse), cancellationToken: cancellationToken);
    }
}