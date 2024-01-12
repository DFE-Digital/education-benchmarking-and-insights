using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Api.Insight.Db;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions;
using EducationBenchmarking.Platform.Functions.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace EducationBenchmarking.Platform.Api.Insight;

[ApiExplorerSettings(GroupName = "Maintained Schools")]
public class MaintainedSchoolFunctions
{
    private readonly ILogger<MaintainedSchoolFunctions> _logger;
    private readonly IMaintainSchoolDb _db;

    public MaintainedSchoolFunctions(ILogger<MaintainedSchoolFunctions> logger, IMaintainSchoolDb db)
    {
        _logger = logger;
        _db = db;
    }

    [FunctionName(nameof(GetMaintainedSchoolAsync))]
    [ProducesResponseType(typeof(Finances), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetMaintainedSchoolAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "maintained-school/{urn}")]
        HttpRequest req,
        string urn)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "Urn", urn }
               }))
        {
            try
            {
                var finances = await _db.Get(urn);

                return finances == null
                    ? new NotFoundResult()
                    : new JsonContentResult(finances);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get maintained school");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}