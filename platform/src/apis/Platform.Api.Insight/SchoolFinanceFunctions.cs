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
using Platform.Api.Insight.Db;
using Platform.Domain;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight;

[ApiExplorerSettings(GroupName = "School Finances")]
public class SchoolFinanceFunctions
{
    private readonly ILogger<SchoolFinanceFunctions> _logger;
    private readonly ISchoolFinancesDb _db;

    public SchoolFinanceFunctions(ILogger<SchoolFinanceFunctions> logger, ISchoolFinancesDb db)
    {
        _logger = logger;
        _db = db;
    }

    [FunctionName(nameof(SchoolSingleAsync))]
    [ProducesResponseType(typeof(FinancesResponseModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SchoolSingleAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "school/{urn}")]
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
                var finances = await _db.Get(urn);

                return finances == null
                    ? new NotFoundResult()
                    : new JsonContentResult(finances);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get school");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(SchoolBalanceHistoryAsync))]
    [ProducesResponseType(typeof(BalanceResponseModel[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string))]
    public async Task<IActionResult> SchoolBalanceHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "school/{urn}/balance/history")]
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
                var queryDimension = req.Query["dimension"].ToString();
                var dimension = Enum.TryParse(queryDimension, true, out Dimension dimensionValue)
                    ? dimensionValue
                    : Dimension.Actuals;

                var finances = await _db.GetBalanceHistory(urn, dimension);
                return new JsonContentResult(finances);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get school balance history");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(SchoolWorkforceHistoryAsync))]
    [ProducesResponseType(typeof(WorkforceResponseModel[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string))]
    public async Task<IActionResult> SchoolWorkforceHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "school/{urn}/workforce/history")]
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
                var queryDimension = req.Query["dimension"].ToString();
                var dimension = Enum.TryParse(queryDimension, true, out Dimension dimensionValue)
                    ? dimensionValue
                    : Dimension.Total;

                var finances = await _db.GetWorkforceHistory(urn, dimension);
                return new JsonContentResult(finances);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get school workforce history");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(SchoolIncomeHistoryAsync))]
    [ProducesResponseType(typeof(IncomeResponseModel[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string))]
    public async Task<IActionResult> SchoolIncomeHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "school/{urn}/income/history")]
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
                var queryDimension = req.Query["dimension"].ToString();
                var dimension = Enum.TryParse(queryDimension, true, out Dimension dimensionValue)
                    ? dimensionValue
                    : Dimension.Actuals;

                var finances = await _db.GetIncomeHistory(urn, dimension);
                return new JsonContentResult(finances);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get school income history");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}