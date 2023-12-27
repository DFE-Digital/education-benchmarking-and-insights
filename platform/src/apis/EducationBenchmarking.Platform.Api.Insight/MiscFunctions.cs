using System;
using System.Collections.Generic;
using System.Net;
using EducationBenchmarking.Platform.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace EducationBenchmarking.Platform.Api.Insight;

[ApiExplorerSettings(GroupName = "Miscellaneous")]
public class MiscFunctions
{
    private readonly ILogger<MiscFunctions> _logger;

    public MiscFunctions(ILogger<MiscFunctions> logger)
    {
        _logger = logger;
    }
    
    [FunctionName(nameof(GetFinanceYears))]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public IActionResult GetFinanceYears(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "finance-years")]
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
                return new JsonContentResult(new { Academies = 2022, MaintainedSchools = 2021});
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get finance years");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}