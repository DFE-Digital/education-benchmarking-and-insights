using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Platform.Functions;
using Platform.Functions.Extensions;
namespace Platform.Api.Insight.BudgetForecast;

[ApiExplorerSettings(GroupName = "Budget Forecast")]
public class BudgetForecastFunctions
{
    private readonly ILogger<BudgetForecastFunctions> _logger;
    private readonly IBudgetForecastService _service;

    public BudgetForecastFunctions(ILogger<BudgetForecastFunctions> logger, IBudgetForecastService service)
    {
        _logger = logger;
        _service = service;
    }

    [FunctionName(nameof(BudgetForecastReturnAsync))]
    [ProducesResponseType(typeof(BudgetForecastReturnResponse[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("runType", "Forecast run type", "default", DataType = typeof(string))]
    [QueryStringParameter("category", "Forecast run category", "Revenue reserve", DataType = typeof(string))]
    [QueryStringParameter("runId", "Forecast run identifier or year", "2022", DataType = typeof(string))]
    public async Task<IActionResult> BudgetForecastReturnAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "budget-forecast/{companyNumber}")]
        HttpRequest req,
        string companyNumber)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<BudgetForecastReturnParameters>();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {
            try
            {
                var results = await _service.GetBudgetForecastReturnsAsync(
                    companyNumber,
                    queryParams.RunType,
                    queryParams.Category,
                    queryParams.RunId);
                return new JsonContentResult(BudgetForecastReturnsResponseFactory.CreateForDefaultRunType(results));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get budget forecast returns");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(BudgetForecastReturnMetricsAsync))]
    [ProducesResponseType(typeof(BudgetForecastReturnMetricResponse[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> BudgetForecastReturnMetricsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "budget-forecast/{companyNumber}/metrics")]
        HttpRequest req,
        string companyNumber)
    {
        var correlationId = req.GetCorrelationId();
        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {
            try
            {
                var result = await _service.GetBudgetForecastReturnMetricsAsync(companyNumber, "default");
                return new JsonContentResult(result.Select(BudgetForecastReturnsResponseFactory.Create));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get budget forecast return metrics");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(BudgetForecastCurrentYearAsync))]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("runType", "Forecast run type", "default", DataType = typeof(string))]
    [QueryStringParameter("category", "Forecast run category", "Revenue reserve", DataType = typeof(string))]
    public async Task<IActionResult> BudgetForecastCurrentYearAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "budget-forecast/{companyNumber}/current-year")]
        HttpRequest req,
        string companyNumber)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<BudgetForecastReturnParameters>();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {
            try
            {
                var year = await _service.GetBudgetForecastCurrentYearAsync(
                    companyNumber,
                    queryParams.RunType,
                    queryParams.Category);
                if (year == null)
                {
                    return new NotFoundResult();
                }

                return new JsonContentResult(year);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get budget forecast current year");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}