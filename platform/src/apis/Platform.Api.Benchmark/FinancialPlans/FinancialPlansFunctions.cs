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

namespace Platform.Api.Benchmark.FinancialPlans;

[ApiExplorerSettings(GroupName = "Financial Plans")]
public class FinancialPlansFunctions
{
    private readonly ILogger<FinancialPlansFunctions> _logger;
    private readonly IFinancialPlansService _service;

    public FinancialPlansFunctions(ILogger<FinancialPlansFunctions> logger, IFinancialPlansService service)
    {
        _logger = logger;
        _service = service;
    }


    [FunctionName(nameof(SingleFinancialPlanAsync))]
    [ProducesResponseType(typeof(FinancialPlanDetails), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SingleFinancialPlanAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "financial-plan/{urn}/{year}")]
        HttpRequest req,
        string urn,
        int year)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "URN", urn },
                   { "Year", year }
               }))
        {
            try
            {
                var plan = await _service.DetailsAsync(urn, year);
                return plan != null
                    ? new JsonContentResult(plan)
                    : new NotFoundResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get financial plan");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(DeploymentPlanAsync))]
    [ProducesResponseType(typeof(FinancialPlanDeployment), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeploymentPlanAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "financial-plan/{urn}/{year}/deployment")]
        HttpRequest req,
        string urn,
        int year)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "URN", urn },
                   { "Year", year }
               }))
        {
            try
            {
                var plan = await _service.DeploymentPlanAsync(urn, year);
                return plan != null
                    ? new JsonContentResult(plan)
                    : new NotFoundResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get deployment plan");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(QueryFinancialPlanAsync))]
    [ProducesResponseType(typeof(IEnumerable<FinancialPlanSummary>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("urns", "List of school URNs to exclude", DataType = typeof(string[]), Required = true)]
    public async Task<IActionResult> QueryFinancialPlanAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "financial-plans")]
        HttpRequest req)
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
                var urns = req.Query["urns"].ToString().Split(",").Where(x => !string.IsNullOrEmpty(x)).ToArray();
                var plans = await _service.QueryAsync(urns);
                return new JsonContentResult(plans);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to query financial plan");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(UpsertFinancialPlanAsync))]
    [ProducesResponseType(typeof(FinancialPlanDetails), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpsertFinancialPlanAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "put", Route = "financial-plan/{urn}/{year}")]
        [RequestBodyType(typeof(FinancialPlanDetails), "The financial plan object")]
        HttpRequest req,
        string urn,
        int year)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "URN", urn },
                   { "Year", year }
               }))
        {
            try
            {
                var body = req.ReadAsJson<FinancialPlanDetails>();

                //TODO : Consider adding request validator
                var result = await _service.UpsertAsync(urn, year, body);

                return result.CreateResponse();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to upsert financial plan");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(RemoveFinancialPlanAsync))]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> RemoveFinancialPlanAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "delete", Route = "financial-plan/{urn}/{year}")]
        HttpRequest req,
        string urn,
        int year)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "URN", urn },
                   { "Year", year }
               }))
        {
            try
            {
                await _service.DeleteAsync(urn, year);
                return new OkResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to delete financial plan");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}