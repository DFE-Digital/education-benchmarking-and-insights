using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DurableTask.Core;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Orchestrator;

public class PipelineFunctions
{
    private readonly ILogger<PipelineFunctions> _logger;
    private readonly IJobStartMessageSender _sender;
    private readonly IPipelineDb _db;

    public PipelineFunctions(ILogger<PipelineFunctions> logger, IJobStartMessageSender sender, IPipelineDb db)
    {
        _logger = logger;
        _sender = sender;
        _db = db;
    }

    [FunctionName(nameof(InitiatePipelineJob))]
    [StorageAccount("PipelineMessageHub:ConnectionString")]
    public async Task InitiatePipelineJob(
        [QueueTrigger("%PipelineMessageHub:JobPendingQueue%")]
        PipelineStartMessage message,
        [DurableClient] IDurableClient client)
    {
        try
        {
            using (_logger.BeginScope(new Dictionary<string, object>
                   {
                       { "Application", Constants.ApplicationName }
                   }))
            {
                var status = await client.GetStatusAsync(message.JobId);

                if (status is not
                    { RuntimeStatus: OrchestrationRuntimeStatus.Pending or OrchestrationRuntimeStatus.Running })
                {
                    await client.StartNewAsync(nameof(PipelineJobOrchestrator), message.JobId, message);
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Initiating pipeline job");
            throw;
        }
    }

    [FunctionName(nameof(PipelineJobFinished))]
    [StorageAccount("PipelineMessageHub:ConnectionString")]
    public async Task PipelineJobFinished(
        [QueueTrigger("%PipelineMessageHub:JobFinishedQueue%")]
        string message,
        [DurableClient] IDurableOrchestrationClient client)
    {
        try
        {
            using (_logger.BeginScope(new Dictionary<string, object>
                       { { "Application", Constants.ApplicationName } }))
            {
                var job = message.FromJson<PipelineFinishMessage>();
                _db.WriteToLog(job.JobId, message);

                if (!string.IsNullOrEmpty(job.JobId))
                {
                    await client.RaiseEventAsync(job.JobId, nameof(PipelineJobFinished), job.Success);
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Finished pipeline job");
            throw;
        }
    }

    [FunctionName(nameof(PipelineJobOrchestrator))]
    public async Task PipelineJobOrchestrator([OrchestrationTrigger] IDurableOrchestrationContext context)
    {
        var input = context.GetInput<PipelineStartMessage>();
        await context.CallActivityAsync(nameof(OnStartJobTrigger), input);

        _logger.LogInformation("{JobId} waiting for finished event", input.JobId);
        var success = await context.WaitForExternalEvent<bool>(nameof(PipelineJobFinished));
        _logger.LogInformation("{JobId} received finished event", input.JobId);

        switch (input.Type)
        {
            case "comparator-set":
            case "custom-data":
                await context.CallActivityAsync(nameof(UpdateStatusTrigger), new PipelineStatus { Id = input.RunId, Success = success });
                break;
        }
    }

    [FunctionName(nameof(OnStartJobTrigger))]
    public async Task OnStartJobTrigger([ActivityTrigger] IDurableActivityContext context)
    {
        var message = context.GetInput<PipelineStartMessage>();
        await _sender.Send(message);
    }

    [FunctionName(nameof(UpdateStatusTrigger))]
    public async Task UpdateStatusTrigger([ActivityTrigger] IDurableActivityContext context)
    {
        var status = context.GetInput<PipelineStatus>();

        _logger.LogInformation("Updating status for {RunId}", status.Id);
        await _db.UpdateStatus(status);
    }

    [FunctionName(nameof(PipelineJobPurgeHistory))]
    public static Task PipelineJobPurgeHistory(
        [DurableClient] IDurableOrchestrationClient client,
        [TimerTrigger("0 0 12 * * *")] TimerInfo timer)
    {
        return client.PurgeInstanceHistoryAsync(
            DateTime.MinValue,
            DateTime.UtcNow.AddDays(-7),
            new List<OrchestrationStatus>
            {
                OrchestrationStatus.Completed,
                OrchestrationStatus.Failed,
                OrchestrationStatus.Canceled,
                OrchestrationStatus.Terminated,
                OrchestrationStatus.Suspended
            });
    }
}