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

[ApiExplorerSettings(GroupName = "Balance")]
public class BalanceFunctions
{
    private readonly ILogger<BalanceFunctions> _logger;
    private readonly IBalanceDb _db;

    public BalanceFunctions(ILogger<BalanceFunctions> logger, IBalanceDb db)
    {
        _logger = logger;
        _db = db;
    }
    
    [FunctionName(nameof(BalanceAllDimensions))]
    [ProducesResponseType(typeof(string[]), (int)HttpStatusCode.OK)]
    public IActionResult BalanceAllDimensions(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "balance/dimensions")]
        HttpRequest req)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {

            return new JsonContentResult(BalanceDimensions.All);
        }
    }
    
    [FunctionName(nameof(BalanceSchoolHistoryAsync))]
    [ProducesResponseType(typeof(CensusResponseModel[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string), Required = true)]
    public async Task<IActionResult> BalanceSchoolHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "balance/school/{urn}/history")]
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
                //TODO: Add validation for dimension
                var dimension = req.Query["dimension"].ToString();
                var result = await _db.GetSchoolHistory(urn, dimension);
                return new JsonContentResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get school balance history");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(SchoolBalanceAsync))]
    [ProducesResponseType(typeof(IncomeResponseModel[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string))]
    public async Task<IActionResult> SchoolBalanceAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "balance/school/{urn}")]
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
                var dimension = req.Query["dimension"].ToString();
                if (!BalanceDimensions.IsValid(dimension) || string.IsNullOrWhiteSpace(dimension))
                {
                    dimension = BalanceDimensions.Actuals;
                }

                var finances = await _db.GetSchool(urn, dimension);
                return finances == null
                    ? new NotFoundResult()
                    : new JsonContentResult(finances);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get school balance");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }


    [FunctionName(nameof(BalanceTrustHistoryAsync))]
    [ProducesResponseType(typeof(CensusResponseModel[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string), Required = true)]
    public async Task<IActionResult> BalanceTrustHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "balance/trust/{companyNumber}/history")]
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
                //TODO: Add validation for dimension
                var dimension = req.Query["dimension"].ToString();
                var result = await _db.GetTrustHistory(companyNumber, dimension);
                return new JsonContentResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get trust balance history");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
    
}