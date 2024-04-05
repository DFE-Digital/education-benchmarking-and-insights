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

[ApiExplorerSettings(GroupName = "Trust Finances")]
public class TrustFinanceFunctions
{
    private readonly ILogger<TrustFinanceFunctions> _logger;
    private readonly ITrustFinancesDb _db;

    public TrustFinanceFunctions(ILogger<TrustFinanceFunctions> logger, ITrustFinancesDb db)
    {
        _logger = logger;
        _db = db;
    }

    [FunctionName(nameof(TrustBalanceHistoryAsync))]
    [ProducesResponseType(typeof(BalanceResponseModel[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string))]
    public async Task<IActionResult> TrustBalanceHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "trust/{companyNumber}/balance/history")]
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
                var queryDimension = req.Query["dimension"].ToString();
                var dimension = Enum.TryParse(queryDimension, true, out Dimension dimensionValue)
                    ? dimensionValue
                    : Dimension.Actuals;

                var finances = await _db.GetBalanceHistory(companyNumber, dimension);
                return new JsonContentResult(finances);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get trust balance history");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(TrustIncomeHistoryAsync))]
    [ProducesResponseType(typeof(IncomeResponseModel[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string))]
    public async Task<IActionResult> TrustIncomeHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "trust/{companyNumber}/income/history")]
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
                var queryDimension = req.Query["dimension"].ToString();
                var dimension = Enum.TryParse(queryDimension, true, out Dimension dimensionValue)
                    ? dimensionValue
                    : Dimension.Actuals;

                var finances = await _db.GetIncomeHistory(companyNumber, dimension);
                return new JsonContentResult(finances);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get trust income history");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(TrustExpenditureHistoryAsync))]
    [ProducesResponseType(typeof(ExpenditureResponseModel[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string))]
    public async Task<IActionResult> TrustExpenditureHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "trust/{urn}/expenditure/history")]
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

                var finances = await _db.GetExpenditureHistory(urn, dimension);
                return new JsonContentResult(finances);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get trust expenditure history");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}