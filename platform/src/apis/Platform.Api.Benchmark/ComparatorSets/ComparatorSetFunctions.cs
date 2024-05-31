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
using Newtonsoft.Json;
using Platform.Domain.Messages;
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
    [ProducesResponseType(typeof(string[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
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
                var comparatorSet = await _service.UserDefinedAsync(urn, identifier);
                if (comparatorSet == null)
                {
                    return new NotFoundResult();
                }

                return comparatorSet.Status != "complete"
                    ? new NoContentResult()
                    : new JsonContentResult(comparatorSet.Set);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get comparator set");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
    
    [FunctionName(nameof(CreateComparatorSetUserDefinedAsync))]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateComparatorSetUserDefinedAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "put", Route = "comparator-set/{urn}/user-defined/{identifier}")]
        [RequestBodyType(typeof(string[]), "The user defined set of schools object")]
        HttpRequest req,
        [Queue("%PipelineMessageHub:JobPendingQueue%", Connection = "PipelineMessageHub:ConnectionString")] IAsyncCollector<string> queue,
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
                var body = req.ReadAsJson<string[]>();
                var comparatorSet = new UserDefinedComparatorSet
                {
                    RunId = identifier,
                    RunType = "default",
                    Set = body,
                    URN = urn
                };
                
                await _service.UpsertUserDefinedSet(comparatorSet);
                var year = await _service.CurrentYearAsync();
                
                var message = new PipelineStartMessage
                {
                    RunId = comparatorSet.RunId,
                    RunType = comparatorSet.RunType,
                    Type = "comparator-set",
                    URN = comparatorSet.URN,
                    Year = year,
                    Payload = new ComparatorSetPayload { Set = comparatorSet.Set }
                };
                await queue.AddAsync(message.ToJson(Formatting.None));
                
                return new AcceptedResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get comparator set");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}