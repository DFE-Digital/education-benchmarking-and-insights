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

[ApiExplorerSettings(GroupName = "Census")]
public class CensusFunctions
{

    private readonly ILogger<CensusFunctions> _logger;
    private readonly ICensusDb _db;

    public CensusFunctions(ILogger<CensusFunctions> logger, ICensusDb db)
    {
        _logger = logger;
        _db = db;
    }

    [FunctionName(nameof(CensusAllCategories))]
    [ProducesResponseType(typeof(string[]), (int)HttpStatusCode.OK)]
    public IActionResult CensusAllCategories(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "census/categories")]
        HttpRequest req)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {

            return new JsonContentResult(CensusCategories.All);
        }
    }

    [FunctionName(nameof(CensusAllDimensions))]
    [ProducesResponseType(typeof(string[]), (int)HttpStatusCode.OK)]
    public IActionResult CensusAllDimensions(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "census/dimensions")]
        HttpRequest req)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {

            return new JsonContentResult(CensusDimensions.All);
        }
    }

    [FunctionName(nameof(CensusAsync))]
    [ProducesResponseType(typeof(CensusResponseModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("category", "Census category", DataType = typeof(string))]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string))]
    public async Task<IActionResult> CensusAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "census/{urn}")]
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
                var category = req.Query["category"].ToString();
                if (!CensusCategories.IsValid(category))
                {
                    category = null;
                }

                var dimension = req.Query["dimension"].ToString();
                if (!CensusDimensions.IsValid(dimension))
                {
                    dimension = CensusDimensions.Total;
                }

                var result = await _db.Get(urn, string.IsNullOrWhiteSpace(category) ? null : category,
                    string.IsNullOrWhiteSpace(dimension) ? CensusDimensions.Total : dimension);

                return result == null
                    ? new NotFoundResult()
                    : new JsonContentResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get census history");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(CensusHistoryAsync))]
    [ProducesResponseType(typeof(CensusResponseModel[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string), Required = true)]
    public async Task<IActionResult> CensusHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "census/{urn}/history")]
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
                var result = await _db.GetHistory(urn, dimension);
                return new JsonContentResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get census history");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(QueryCensusAsync))]
    [ProducesResponseType(typeof(CensusResponseModel[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("urns", "List of school URNs", DataType = typeof(string), Required = true)]
    [QueryStringParameter("category", "Census category", DataType = typeof(string), Required = true)]
    [QueryStringParameter("dimension", "Value dimension", DataType = typeof(string), Required = true)]
    public async Task<IActionResult> QueryCensusAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "census")]
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
                //TODO: Add validation for urns, category and dimension
                var urns = req.Query["urns"].ToString().Split(",");
                var category = req.Query["category"].ToString();
                var dimension = req.Query["dimension"].ToString();
                var finances = await _db.Get(urns, category, dimension);
                return new JsonContentResult(finances);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get census");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}