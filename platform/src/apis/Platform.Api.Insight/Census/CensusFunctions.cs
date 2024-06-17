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

namespace Platform.Api.Insight.Census;

[ApiExplorerSettings(GroupName = "Census")]
public class CensusFunctions
{

    private readonly ILogger<CensusFunctions> _logger;
    private readonly ICensusService _service;

    public CensusFunctions(ILogger<CensusFunctions> logger, ICensusService service)
    {
        _logger = logger;
        _service = service;
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
    [ProducesResponseType(typeof(CensusResponse), (int)HttpStatusCode.OK)]
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
        var queryParams = req.GetParameters<CensusParameters>();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {
            try
            {
                var result = await _service.GetAsync(urn);
                return result == null
                    ? new NotFoundResult()
                    : new JsonContentResult(CensusResponseFactory.Create(result, queryParams.Category, queryParams.Dimension));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get census");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(CensusHistoryAsync))]
    [ProducesResponseType(typeof(CensusHistoryResponse[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string), Required = true)]
    public async Task<IActionResult> CensusHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "census/{urn}/history")]
        HttpRequest req,
        string urn)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<CensusParameters>();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {
            try
            {
                //TODO: Add validation for dimension
                var result = await _service.GetHistoryAsync(urn);
                return new JsonContentResult(result.Select(x => CensusResponseFactory.Create(x, queryParams.Dimension)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get census history");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(QueryCensusAsync))]
    [ProducesResponseType(typeof(CensusResponse[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("urns", "List of school URNs", DataType = typeof(string[]), Required = true)]
    [QueryStringParameter("category", "Census category", DataType = typeof(string), Required = true)]
    [QueryStringParameter("dimension", "Value dimension", DataType = typeof(string), Required = true)]
    public async Task<IActionResult> QueryCensusAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "census")]
        HttpRequest req)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<CensusParameters>();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {
            try
            {
                //TODO: Add validation for urns, category and dimension
                var result = await _service.QueryAsync(queryParams.Schools);
                return new JsonContentResult(result.Select(x => CensusResponseFactory.Create(x, queryParams.Category, queryParams.Dimension)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get census");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}