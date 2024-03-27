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

[ApiExplorerSettings(GroupName = "Academies")]
public class AcademyFunctions
{
    private readonly ILogger<AcademyFunctions> _logger;
    private readonly IAcademyDb _db;

    public AcademyFunctions(ILogger<AcademyFunctions> logger, IAcademyDb db)
    {
        _logger = logger;
        _db = db;
    }

    [FunctionName(nameof(SingleAcademyAsync))]
    [ProducesResponseType(typeof(FinancesResponseModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SingleAcademyAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "academy/{urn}")]
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
                var finances = await _db.Get(urn);

                return finances == null
                    ? new NotFoundResult()
                    : new JsonContentResult(finances);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get academy");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}