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

namespace Platform.Api.Insight.Schools;

[ApiExplorerSettings(GroupName = "Schools")]
public class SchoolsFunctions
{
    private readonly ILogger<SchoolsFunctions> _logger;
    private readonly ISchoolsService _service;

    public SchoolsFunctions(ILogger<SchoolsFunctions> logger, ISchoolsService service)
    {
        _logger = logger;
        _service = service;
    }

    [FunctionName(nameof(SchoolsCharacteristicsAsync))]
    [ProducesResponseType(typeof(SchoolCharacteristic), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SchoolsCharacteristicsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "school/{urn}/characteristics")]
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
                var result = await _service.CharacteristicAsync(urn);
                return result == null
                    ? new NotFoundResult()
                    : new JsonContentResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get school characteristics");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(QuerySchoolsCharacteristicsAsync))]
    [ProducesResponseType(typeof(SchoolCharacteristic[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("urns", "List of school URNs", DataType = typeof(string[]), Required = true)]
    public async Task<IActionResult> QuerySchoolsCharacteristicsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "schools/characteristics")]
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
                var schools = await _service.QueryCharacteristicAsync(urns);

                return new JsonContentResult(schools);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get schools characteristics");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}