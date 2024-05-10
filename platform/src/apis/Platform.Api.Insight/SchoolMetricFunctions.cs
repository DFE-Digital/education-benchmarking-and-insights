using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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

[ApiExplorerSettings(GroupName = "School Metrics")]
public class SchoolMetricFunctions
{
    private readonly ILogger<SchoolMetricFunctions> _logger;
    private readonly ISchoolMetricsDb _db;

    public SchoolMetricFunctions(ILogger<SchoolMetricFunctions> logger, ISchoolMetricsDb db)
    {
        _logger = logger;
        _db = db;
    }

    [FunctionName(nameof(MetricSingleAsync))]
    [ProducesResponseType(typeof(FloorAreaResponseModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> MetricSingleAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "metric/{urn}/floor-area")]
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
                var floorArea = await _db.FloorArea(urn);

                return floorArea == null
                    ? new NotFoundResult()
                    : new JsonContentResult(floorArea);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get floor area");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}