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

[ApiExplorerSettings(GroupName = "Workforce")]
public class WorkforceFunctions
{

    private readonly ILogger<WorkforceFunctions> _logger;
    private readonly IWorkforceDb _db;

    public WorkforceFunctions(ILogger<WorkforceFunctions> logger, IWorkforceDb db)
    {
        _logger = logger;
        _db = db;
    }

    [FunctionName(nameof(WorkforceHistoryAsync))]
    [ProducesResponseType(typeof(WorkforceResponseModel[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string))]
    public async Task<IActionResult> WorkforceHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "workforce/{urn}/history")]
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
                var dimension = GetDimensionFromQuery(req);
                var result = await _db.GetHistory(urn, dimension);
                return new JsonContentResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get workforce history");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(QueryWorkforceAsync))]
    [ProducesResponseType(typeof(WorkforceResponseModel[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("urns", "List of school URNs", DataType = typeof(string), Required = true)]
    [QueryStringParameter("category", "Workforce category", DataType = typeof(string), Required = true)]
    [QueryStringParameter("dimension", "Value dimension", DataType = typeof(string))]
    public async Task<IActionResult> QueryWorkforceAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "workforce")]
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
                var dimension = GetDimensionFromQuery(req);
                var finances = await _db.Get(urns, category, dimension);
                return new JsonContentResult(finances);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get workforce");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }


    private static WorkforceDimension GetDimensionFromQuery(HttpRequest req)
    {
        var queryDimension = req.Query["dimension"].ToString();
        var dimension = Enum.TryParse(queryDimension, true, out WorkforceDimension dimensionValue)
            ? dimensionValue
            : WorkforceDimension.Total;
        return dimension;
    }
}