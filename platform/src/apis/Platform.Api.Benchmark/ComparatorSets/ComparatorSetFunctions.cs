using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Benchmark.ComparatorSets;

[ApiExplorerSettings(GroupName = "Comparator Set")]
public class ComparatorSetFunctions
{
    private readonly ILogger<ComparatorSetFunctions> _logger;
    private readonly IComparatorSetService _service;

    public ComparatorSetFunctions(IComparatorSetService service, ILogger<ComparatorSetFunctions> logger)
    {
        _service = service;
        _logger = logger;
    }

    [FunctionName(nameof(ComparatorSetDefaultAsync))]
    [ProducesResponseType(typeof(DefaultComparatorSet), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ComparatorSetDefaultAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "comparator-set/{urn}/default")]
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
                var set = await _service.DefaultAsync(urn);

                return new JsonContentResult(set);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get default comparator set");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }


    [FunctionName(nameof(ComparatorSetUserDefinedAsync))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ComparatorSetUserDefinedAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "comparator-set/{urn}/user-defined/{identifier}")]
        HttpRequest req,
        string urn,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "URN", urn },
                   { "Identifier", identifier}
               }))
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get comparator set");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }


    [FunctionName(nameof(CreateComparatorSetUserDefinedAsync))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateComparatorSetUserDefinedAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "comparator-set/{urn}/user-defined/{identifier}")]
        HttpRequest req,
        string urn,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "URN", urn },
                   { "Identifier", identifier}
               }))
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get comparator set");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(StatusComparatorSetUserDefinedAsync))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> StatusComparatorSetUserDefinedAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "comparator-set/{urn}/user-defined/{identifier}/status")]
        HttpRequest req,
        string urn,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "URN", urn },
                   { "Identifier", identifier}
               }))
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get comparator set");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}