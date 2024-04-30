using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Platform.Api.Benchmark.Db;
using Platform.Domain;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Benchmark;

[ApiExplorerSettings(GroupName = "Financial Plan")]
public class FinancialPlanFunctions
{
    private readonly ILogger<FinancialPlanFunctions> _logger;
    private readonly IFinancialPlanDb _db;

    public FinancialPlanFunctions(ILogger<FinancialPlanFunctions> logger, IFinancialPlanDb db)
    {
        _logger = logger;
        _db = db;
    }


    [FunctionName(nameof(SingleFinancialPlanAsync))]
    [ProducesResponseType(typeof(FinancialPlanInputResponseModel), (int)HttpStatusCode.OK)]
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
                   { "CorrelationID", correlationId }
               }))
        {
            try
            {
                var plan = await _db.SingleFinancialPlanInput(urn, year);
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

    [FunctionName(nameof(QueryFinancialPlanAsync))]
    [ProducesResponseType(typeof(FinancialPlanInputResponseModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> QueryFinancialPlanAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "financial-plan/{urn}")]
        HttpRequest req,
        string urn)
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
                var plans = await _db.QueryFinancialPlan(urn);
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
    [ProducesResponseType(typeof(FinancialPlanInputResponseModel), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpsertFinancialPlanAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "put", Route = "financial-plan/{urn}/{year}")]
        [RequestBodyType(typeof(FinancialPlanInputRequestModel), "The financial plan object")]
        HttpRequest req,
        string urn,
        int year)
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
                var body = req.ReadAsJson<FinancialPlanInputRequestModel>();

                //TODO : Consider adding request validator
                var result = await _db.UpsertFinancialPlan(urn, year, body);

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
                   { "CorrelationID", correlationId }
               }))
        {
            try
            {
                await _db.DeleteFinancialPlan(urn, year);
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