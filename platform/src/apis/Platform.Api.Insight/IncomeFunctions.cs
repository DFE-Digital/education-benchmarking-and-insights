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

[ApiExplorerSettings(GroupName = "Income")]
public class IncomeFunctions
{

    private readonly ILogger<IncomeFunctions> _logger;
    private readonly IIncomeDb _db;

    public IncomeFunctions(ILogger<IncomeFunctions> logger, IIncomeDb db)
    {
        _logger = logger;
        _db = db;
    }

    [FunctionName(nameof(IncomeAllCategories))]
    [ProducesResponseType(typeof(string[]), (int)HttpStatusCode.OK)]
    public IActionResult IncomeAllCategories(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/categories")]
        HttpRequest req)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {

            return new JsonContentResult(IncomeCategories.All);
        }
    }

    [FunctionName(nameof(IncomeAllDimensions))]
    [ProducesResponseType(typeof(string[]), (int)HttpStatusCode.OK)]
    public IActionResult IncomeAllDimensions(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/dimensions")]
        HttpRequest req)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {

            return new JsonContentResult(IncomeDimensions.All);
        }
    }

    [FunctionName(nameof(IncomeSchoolHistoryAsync))]
    [ProducesResponseType(typeof(CensusResponseModel[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string), Required = true)]
    public async Task<IActionResult> IncomeSchoolHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/school/{urn}/history")]
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
                _logger.LogError(e, "Failed to get school income history");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(SchoolIncomeAsync))]
    [ProducesResponseType(typeof(IncomeResponseModel[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string))]
    public async Task<IActionResult> SchoolIncomeAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/school/{urn}")]
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
                if (!IncomeDimensions.IsValid(dimension) || string.IsNullOrWhiteSpace(dimension))
                {
                    dimension = IncomeDimensions.Actuals;
                }

                var finances = await _db.GetSchool(urn, dimension);
                return finances == null
                    ? new NotFoundResult()
                    : new JsonContentResult(finances);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get school income");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }


    [FunctionName(nameof(IncomeTrustHistoryAsync))]
    [ProducesResponseType(typeof(CensusResponseModel[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string), Required = true)]
    public async Task<IActionResult> IncomeTrustHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/trust/{companyNumber}/history")]
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
                _logger.LogError(e, "Failed to get trust income history");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}