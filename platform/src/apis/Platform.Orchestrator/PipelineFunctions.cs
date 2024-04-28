using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Platform.Domain;
using Platform.Domain.Messages;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Orchestrator;

public class PipelineFunctions
{
    private readonly ILogger<PipelineFunctions> _logger;
    private readonly IJobStartMessageSender _sender;


    public PipelineFunctions(ILogger<PipelineFunctions> logger, IJobStartMessageSender sender)
    {
        _logger = logger;
        _sender = sender;
    }


    [FunctionName(nameof(InitiatePipelineJob))]
    public async Task<IActionResult> InitiatePipelineJob(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "pipeline")]
        [RequestBodyType(typeof(PipelineJobRequestModel), "The pipeline job object")] HttpRequest req,
        [DurableClient] IDurableClient client)
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
                var body = req.ReadAsJson<PipelineJobRequestModel>();
                var status = await client.GetStatusAsync(body.JobId);

                if (status is { RuntimeStatus: OrchestrationRuntimeStatus.Pending or OrchestrationRuntimeStatus.Running })
                {
                    return client.CreateCheckStatusResponse(req, body.JobId);
                }

                await client.StartNewAsync(nameof(PipelineJobOrchestrator), body.JobId, body);

                return client.CreateCheckStatusResponse(req, body.JobId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Initiating pipeline job");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(QueryPipelineJobState))]
    public async Task<IActionResult> QueryPipelineJobState(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "pipeline/{identifier}/state")]
        HttpRequest req,
        [DurableClient] IDurableClient client,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();
        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName }, { "CorrelationID", correlationId }
               }))
        {
            try
            {
                var status = await client.GetStatusAsync(identifier);
                return status != null
                    ? new JsonContentResult(status.RuntimeStatus)
                    : new NotFoundResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Query pipeline job state failed");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }


    [FunctionName(nameof(PipelineJobFinished))]
    [StorageAccount("PipelineMessageHub:ConnectionString")]
    public async Task PipelineJobFinished(
        [QueueTrigger("%PipelineMessageHub:JobFinishedQueue%")]
        PipelineMessage message,
        [DurableClient] IDurableOrchestrationClient client)
    {

        await client.RaiseEventAsync(message.JobId, nameof(PipelineJobFinished));
    }


    [FunctionName(nameof(PipelineJobOrchestrator))]
    public async Task PipelineJobOrchestrator([OrchestrationTrigger] IDurableOrchestrationContext context)
    {
        var input = context.GetInput<PipelineJobRequestModel>();
        await context.CallActivityAsync(nameof(OnStartJobTrigger), input);
        await context.WaitForExternalEvent(nameof(PipelineJobFinished));
    }

    [FunctionName(nameof(OnStartJobTrigger))]
    public async Task OnStartJobTrigger([ActivityTrigger] IDurableActivityContext context)
    {
        var input = context.GetInput<PipelineJobRequestModel>();
        await _sender.Send(new PipelineMessage
        {
            JobId = input.JobId
        });
    }
}