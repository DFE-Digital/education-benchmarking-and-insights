using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Platform.Api.Benchmark.Db;
using Platform.Domain;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Benchmark;

[ApiExplorerSettings(GroupName = "Comparator Set")]
public class ComparatorSetFunctions
{
    private readonly ILogger<ComparatorSetFunctions> _logger;
    private readonly IComparatorSetDb _db;

    public ComparatorSetFunctions(IComparatorSetDb db, ILogger<ComparatorSetFunctions> logger)
    {
        _db = db;
        _logger = logger;
    }

    [FunctionName(nameof(ComparatorSetAsync))]
    [ProducesResponseType(typeof(ComparatorSetResponseModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ComparatorSetAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "comparator-set/{urn}")]
        HttpRequest req,
        string urn)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "URN", urn }
               }))
        {
            try
            {
                var set = await _db.Get(urn);

                return new JsonContentResult(set);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get comparator set");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}