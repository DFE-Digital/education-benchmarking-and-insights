using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Insight.Features.BudgetForecast.Parameters;
using Platform.Api.Insight.Features.BudgetForecast.Responses;
using Platform.Api.Insight.Features.BudgetForecast.Services;
using Platform.Functions.Extensions;
using Platform.OpenApi;
using Platform.Api.Insight.OpenApi;

namespace Platform.Api.Insight.Features.BudgetForecast;

public class GetBudgetForecastReturnFunction(IBudgetForecastService service)
{
    [Function(nameof(GetBudgetForecastReturnFunction))]
    [OpenApiOperation(nameof(GetBudgetForecastReturnFunction), Constants.Features.BudgetForecast, Deprecated = true)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("runType", In = ParameterLocation.Query, Description = "Forecast run type", Type = typeof(string), Example = typeof(OpenApiExamples.BudgetForecastRunType))]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Forecast run category", Type = typeof(string), Required = false, Example = typeof(OpenApiExamples.BudgetForecastRunCategory))]
    [OpenApiParameter("runId", In = ParameterLocation.Query, Description = "Forecast run identifier or year", Type = typeof(string), Required = true, Example = typeof(OpenApiExamples.BudgetForecastRunId))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(BudgetForecastReturnResponse[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.Return)] HttpRequestData req,
        string companyNumber,
        CancellationToken cancellationToken = default)
    {
        var queryParams = req.GetParameters<BudgetForecastReturnParameters>();

        var bfr = await service.GetBudgetForecastReturnsAsync(companyNumber, queryParams.RunType, queryParams.Category, queryParams.RunId, cancellationToken);

        var ar = await service.GetActualReturnsAsync(companyNumber, queryParams.Category, queryParams.RunId, cancellationToken);

        return await req.CreateJsonResponseAsync(Mapper.MapToApiResponse(bfr, ar), cancellationToken);
    }
}