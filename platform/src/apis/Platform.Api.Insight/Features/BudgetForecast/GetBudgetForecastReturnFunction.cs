using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Insight.Features.BudgetForecast.Parameters;
using Platform.Api.Insight.Features.BudgetForecast.Responses;
using Platform.Api.Insight.Features.BudgetForecast.Services;
using Platform.Api.Insight.OpenApi.Examples;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Insight.Features.BudgetForecast;

public class GetBudgetForecastReturnFunction(IBudgetForecastService service)
{
    [Function(nameof(GetBudgetForecastReturnFunction))]
    [OpenApiOperation(nameof(GetBudgetForecastReturnFunction), Constants.Features.BudgetForecast)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("runType", In = ParameterLocation.Query, Description = "Forecast run type", Type = typeof(string), Example = typeof(ExampleBudgetForecastRunType))]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Forecast run category", Type = typeof(string), Required = false, Example = typeof(ExampleBudgetForecastRunCategory))]
    [OpenApiParameter("runId", In = ParameterLocation.Query, Description = "Forecast run identifier or year", Type = typeof(string), Required = true, Example = typeof(ExampleBudgetForecastRunId))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(BudgetForecastReturnResponse[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.Return)] HttpRequestData req,
        string companyNumber)
    {
        var queryParams = req.GetParameters<BudgetForecastReturnParameters>();

        var bfr = await service.GetBudgetForecastReturnsAsync(
            companyNumber,
            queryParams.RunType,
            queryParams.Category,
            queryParams.RunId);

        var ar = await service.GetActualReturnsAsync(
            companyNumber,
            queryParams.Category,
            queryParams.RunId);

        return await req.CreateJsonResponseAsync(BudgetForecastReturnsResponseFactory.CreateForDefaultRunType(bfr, ar));
    }
}