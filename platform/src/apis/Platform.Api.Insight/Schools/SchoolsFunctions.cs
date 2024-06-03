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
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.Schools;

[ApiExplorerSettings(GroupName = "Schools")]
public class SchoolsFunctions
{
    private readonly ILogger<SchoolsFunctions> _logger;

    public SchoolsFunctions(ILogger<SchoolsFunctions> logger)
    {
        _logger = logger;
    }

    [FunctionName(nameof(QuerySchoolsPupilCharacteristicsAsync))]
    [ProducesResponseType(typeof(SchoolCharacteristic[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("urns", "List of school URNs", DataType = typeof(string[]), Required = true)]
    public async Task<IActionResult> QuerySchoolsPupilCharacteristicsAsync(
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
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get schools characteristics");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}