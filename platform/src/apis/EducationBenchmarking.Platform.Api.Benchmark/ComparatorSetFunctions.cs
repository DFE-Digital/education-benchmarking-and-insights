using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Api.Benchmark.Db;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions;
using EducationBenchmarking.Platform.Functions.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace EducationBenchmarking.Platform.Api.Benchmark;

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
    
    [FunctionName(nameof(CreateComparatorSetAsync))]
    [ProducesResponseType(typeof(ComparatorSet), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateComparatorSetAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "comparator-set")]
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
                var set = await _db.CreateSet();

                return new JsonContentResult(set);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create comparator set");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}