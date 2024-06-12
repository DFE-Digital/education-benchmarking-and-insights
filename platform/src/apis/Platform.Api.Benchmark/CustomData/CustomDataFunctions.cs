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
using Platform.Domain.Messages;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Benchmark.CustomData;


[ApiExplorerSettings(GroupName = "Custom Data")]
public class CustomDataFunctions
{
    private readonly ILogger<CustomDataFunctions> _logger;
    private readonly ICustomDataService _service;

    public CustomDataFunctions(ILogger<CustomDataFunctions> logger, ICustomDataService service)
    {
        _logger = logger;
        _service = service;
    }

    [FunctionName(nameof(SchoolCustomDataAsync))]
    [ProducesResponseType(typeof(CustomDataSchool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SchoolCustomDataAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "custom-data/school/{urn}/{identifier}")]
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
                   { "Identifier", identifier }
               }))
        {
            try
            {
                var data = await _service.CustomDataSchoolAsync(urn, identifier);
                return data == null
                    ? new NotFoundResult()
                    : new JsonContentResult(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get user defined school comparator set");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(CreateSchoolCustomDataAsync))]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateSchoolCustomDataAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "put", Route = "custom-data/school/{urn}/{identifier}")]
        [RequestBodyType(typeof(CustomDataRequest), "The user defined set of schools object")]
        HttpRequest req,
        [Queue("%PipelineMessageHub:JobPendingQueue%", Connection = "PipelineMessageHub:ConnectionString")]
        IAsyncCollector<string> queue,
        string urn,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "URN", urn },
                   { "Identifier", identifier }
               }))
        {
            try
            {
                var body = req.ReadAsJson<CustomDataRequest>();
                var data = new CustomDataSchool();

                await _service.UpsertCustomDataAsync(data);
                await _service.UpsertUserDataAsync(CustomDataUserData.CompleteSchool(identifier, body.UserId, urn));
                var year = await _service.CurrentYearAsync();

                var message = new PipelineStartMessage
                {
                    RunId = data.RunId,
                    RunType = data.RunType,
                    Type = "custom-data",
                    URN = data.URN,
                    Year = int.Parse(year),
                    Payload = new CustomDataPayload()
                };

                await queue.AddAsync(message.ToJson());
                return new AcceptedResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to upsert school custom data");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(RemoveSchoolCustomDataAsync))]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> RemoveSchoolCustomDataAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "delete",Route = "custom-data/school/{urn}/{identifier}")]
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
                   { "Identifier", identifier }
               }))
        {
            try
            {
                var data = await _service.CustomDataSchoolAsync(urn, identifier);
                if (data == null)
                {
                    return new NotFoundResult();
                }

                await _service.DeleteSchoolAsync(data);
                return new OkResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to delete school custom data");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}