using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Platform.Api.Insight.OpenApi.Examples;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
namespace Platform.Api.Insight.BudgetForecast;

public class BudgetForecastFunctions(ILogger<BudgetForecastFunctions> logger, IBudgetForecastService service)
{
    [Function(nameof(BudgetForecastReturnAsync))]
    [OpenApiOperation(nameof(BudgetForecastReturnAsync), "Budget Forecast")]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("runType", In = ParameterLocation.Query, Description = "Forecast run type", Type = typeof(string), Example = typeof(ExampleBudgetForecastRunType))]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Forecast run category", Type = typeof(string), Required = false, Example = typeof(ExampleBudgetForecastRunCategory))]
    [OpenApiParameter("runId", In = ParameterLocation.Query, Description = "Forecast run identifier or year", Type = typeof(string), Required = false, Example = typeof(ExampleBudgetForecastRunId))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(BudgetForecastReturnResponse[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> BudgetForecastReturnAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "budget-forecast/{companyNumber}")] HttpRequestData req,
        string companyNumber)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<BudgetForecastReturnParameters>();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   }
               }))
        {
            try
            {
                var results = await service.GetBudgetForecastReturnsAsync(
                    companyNumber,
                    queryParams.RunType,
                    queryParams.Category,
                    queryParams.RunId);
                return await req.CreateJsonResponseAsync(BudgetForecastReturnsResponseFactory.CreateForDefaultRunType(results));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get budget forecast returns");
                return req.CreateErrorResponse();
            }
        }
    }

    [Function(nameof(BudgetForecastReturnMetricsAsync))]
    [OpenApiOperation(nameof(BudgetForecastReturnMetricsAsync), "Budget Forecast")]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(BudgetForecastReturnMetricResponse[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> BudgetForecastReturnMetricsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "budget-forecast/{companyNumber}/metrics")] HttpRequestData req,
        string companyNumber)
    {
        var correlationId = req.GetCorrelationId();
        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   }
               }))
        {
            try
            {
                var result = await service.GetBudgetForecastReturnMetricsAsync(companyNumber, "default");
                return await req.CreateJsonResponseAsync(result.Select(BudgetForecastReturnsResponseFactory.Create));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get budget forecast return metrics");
                return req.CreateErrorResponse();
            }
        }
    }

    [Function(nameof(BudgetForecastCurrentYearAsync))]
    [OpenApiOperation(nameof(BudgetForecastCurrentYearAsync), "Budget Forecast")]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("runType", In = ParameterLocation.Query, Description = "Forecast run type", Type = typeof(string), Example = typeof(ExampleBudgetForecastRunType))]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Forecast run category", Type = typeof(string), Required = false, Example = typeof(ExampleBudgetForecastRunCategory))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(int))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> BudgetForecastCurrentYearAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "budget-forecast/{companyNumber}/current-year")] HttpRequestData req,
        string companyNumber)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<BudgetForecastReturnParameters>();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   }
               }))
        {
            try
            {
                var year = await service.GetBudgetForecastCurrentYearAsync(
                    companyNumber,
                    queryParams.RunType,
                    queryParams.Category);
                if (year == null)
                {
                    return req.CreateNotFoundResponse();
                }

                return await req.CreateJsonResponseAsync(year);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get budget forecast current year");
                return req.CreateErrorResponse();
            }
        }
    }
}