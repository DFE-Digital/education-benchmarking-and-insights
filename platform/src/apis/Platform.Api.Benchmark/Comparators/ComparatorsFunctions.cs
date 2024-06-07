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
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Benchmark.Comparators;

[ApiExplorerSettings(GroupName = "Comparators")]
public class ComparatorsFunctions
{
    private readonly ILogger<ComparatorsFunctions> _logger;
    private readonly IComparatorSchoolsService _schoolsService;
    private readonly IComparatorTrustsService _trustsService;
    public ComparatorsFunctions(ILogger<ComparatorsFunctions> logger, IComparatorSchoolsService schoolsService, IComparatorTrustsService trustsService)
    {
        _logger = logger;
        _schoolsService = schoolsService;
        _trustsService = trustsService;
    }

    [FunctionName(nameof(SchoolComparatorsAsync))]
    [ProducesResponseType(typeof(ComparatorSchools), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SchoolComparatorsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "comparators/schools")]
        [RequestBodyType(typeof(ComparatorSchoolsRequest), "The comparator characteristics object")]
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
                var body = req.ReadAsJson<ComparatorSchoolsRequest>();
                //TODO : Add request validation
                var comparators = await _schoolsService.ComparatorsAsync(body);
                return new JsonContentResult(comparators);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create school comparators");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(TrustComparatorsAsync))]
    [ProducesResponseType(typeof(ComparatorSchools), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> TrustComparatorsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "comparators/trusts")]
        [RequestBodyType(typeof(ComparatorTrustsRequest), "The comparator characteristics object")]
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
                var body = req.ReadAsJson<ComparatorTrustsRequest>();
                //TODO : Add request validation
                var comparators = await _trustsService.ComparatorsAsync(body);
                return new JsonContentResult(comparators);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create trust comparators");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}