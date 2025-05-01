using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Insight.Features.BudgetForecast.Services;
using Platform.Api.Insight.OpenApi.Examples;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Insight.Features.BudgetForecast;

public class GetBudgetForecastCurrentYearFunction(IBudgetForecastService service)
{
    [Function(nameof(GetBudgetForecastCurrentYearFunction))]
    [OpenApiOperation(nameof(GetBudgetForecastCurrentYearFunction), Constants.Features.BudgetForecast)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("runType", In = ParameterLocation.Query, Description = "Forecast run type", Type = typeof(string), Example = typeof(ExampleBudgetForecastRunType))]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Forecast run category", Type = typeof(string), Required = false, Example = typeof(ExampleBudgetForecastRunCategory))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(int))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.CurrentYear)] HttpRequestData req,
        string companyNumber)
    {
        var year = await service.GetBudgetForecastCurrentYearAsync();
        if (year == null)
        {
            return req.CreateNotFoundResponse();
        }

        return await req.CreateJsonResponseAsync(year);
    }
}