using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Orchestrator.Search;
namespace Platform.Orchestrator;

public class PipelineFunctions(ILogger<PipelineFunctions> logger, IPipelineDb db, IPipelineSearch search)
{
    [Function(nameof(InitiatePipelineJob))]
    public async Task InitiatePipelineJob(
        [QueueTrigger("%PipelineMessageHub:JobPendingQueue%", Connection = "PipelineMessageHub:ConnectionString")] PipelineStartMessage message,
        [DurableClient] DurableTaskClient client)
    {
        try
        {
            using (logger.BeginScope(new Dictionary<string, object>
                   {
                       {
                           "Application", Constants.ApplicationName
                       }
                   }))
            {
                var status = await client.GetInstanceAsync(message.JobId!);

                if (status is not
                    { RuntimeStatus: OrchestrationRuntimeStatus.Pending or OrchestrationRuntimeStatus.Running })
                {
                    await client.ScheduleNewOrchestrationInstanceAsync(nameof(PipelineJobOrchestrator), message, new StartOrchestrationOptions(message.JobId));
                }
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "Initiating pipeline job");
            throw;
        }
    }

    [Function(nameof(PipelineJobFinished))]
    public async Task PipelineJobFinished(
        [QueueTrigger("%PipelineMessageHub:JobFinishedQueue%", Connection = "PipelineMessageHub:ConnectionString")] string message,
        [DurableClient] DurableTaskClient client)
    {
        try
        {
            using (logger.BeginScope(new Dictionary<string, object>
                   {
                       {
                           "Application", Constants.ApplicationName
                       }
                   }))
            {
                var job = message.FromJson<PipelineFinishMessage>();
                await db.WriteToLog(job.JobId, message);

                if (!string.IsNullOrEmpty(job.JobId))
                {
                    await client.RaiseEventAsync(job.JobId, nameof(PipelineJobFinished), job.Success);
                }
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "Finished pipeline job");
            throw;
        }
    }

    [Function(nameof(PipelineJobOrchestrator))]
    public async Task PipelineJobOrchestrator([OrchestrationTrigger] TaskOrchestrationContext context)
    {
        var input = context.GetInput<PipelineStartMessage>();
        switch (input?.Type)
        {
            case PipelineJobType.ComparatorSet:
            case PipelineJobType.CustomData:
                await context.CallActivityAsync(nameof(OnStartCustomJobTrigger), input);
                break;
            case PipelineJobType.Default:
                await context.CallActivityAsync(nameof(OnStartDefaultJobTrigger), input);
                break;
        }

        logger.LogInformation("Waiting for finished event for {JobId}", input?.JobId);
        var success = await context.WaitForExternalEvent<bool>(nameof(PipelineJobFinished));
        logger.LogInformation("Received finished event for {JobId}", input?.JobId);

        switch (input?.Type)
        {
            case PipelineJobType.ComparatorSet:
            case PipelineJobType.CustomData:
                await context.CallActivityAsync(nameof(UpdateStatusTrigger), new PipelineStatus
                {
                    Id = input.RunId,
                    Success = success
                });
                break;
            case PipelineJobType.Default:
                await context.CallActivityAsync(nameof(RunIndexerTrigger), new PipelineStatus
                {
                    Id = input.JobId,
                    Success = success
                });
                break;
        }
    }

    /// <summary>
    ///     Messages on the <c>data-pipeline-job-default-start</c> queue should already have been scheduled in the Orchestrator
    ///     and thus have a <see cref="PipelineStartMessage.JobId">JobId</see> in the payload. Without this property the
    ///     Orchestrator will not raise the 'Finished' event at a later date from a message on the
    ///     <c>data-pipeline-job-finished</c> queue. Messages should therefore be added to the <c>data-pipeline-job-pending</c>
    ///     queue instead of <c>data-pipeline-job-start</c> directly so that this function is triggered to forward the
    ///     message in the correct format via <see cref="PipelineJobOrchestrator" />.
    /// </summary>
    [Function(nameof(OnStartDefaultJobTrigger))]
    [QueueOutput("%PipelineMessageHub:JobDefaultStartQueue%", Connection = "PipelineMessageHub:ConnectionString")]
    public string[] OnStartDefaultJobTrigger([ActivityTrigger] PipelineStartMessage message)
    {
        logger.LogInformation("Forwarding {JobId} to {StartQueue} start queue", message.JobId, "default");
        return [message.ToJson()];
    }

    [Function(nameof(OnStartCustomJobTrigger))]
    [QueueOutput("%PipelineMessageHub:JobCustomStartQueue%", Connection = "PipelineMessageHub:ConnectionString")]
    public string[] OnStartCustomJobTrigger([ActivityTrigger] PipelineStartMessage message)
    {
        logger.LogInformation("Forwarding {JobId} to {StartQueue} start queue", message.JobId, "custom");
        return [message.ToJson()];
    }

    [Function(nameof(UpdateStatusTrigger))]
    public async Task UpdateStatusTrigger([ActivityTrigger] PipelineStatus status)
    {
        logger.LogInformation("Updating status for {RunId}", status.Id);
        await db.UpdateStatus(status);
    }

    [Function(nameof(PipelineJobPurgeHistory))]
    public static Task PipelineJobPurgeHistory(
        [DurableClient] DurableTaskClient client,
        [TimerTrigger("0 0 12 * * *")] TimerInfo timer) => client.PurgeAllInstancesAsync(
        new PurgeInstancesFilter(
            DateTime.MinValue,
            DateTime.UtcNow.AddDays(-7),
            new List<OrchestrationRuntimeStatus>
            {
                OrchestrationRuntimeStatus.Completed,
                OrchestrationRuntimeStatus.Failed,
                OrchestrationRuntimeStatus.Terminated,
                OrchestrationRuntimeStatus.Suspended
            }));

    [Function(nameof(RunIndexerTrigger))]
    public async Task RunIndexerTrigger([ActivityTrigger] PipelineStatus status)
    {
        if (!status.Success)
        {
            logger.LogInformation("Not updating indexers due to failed status from {RunId}", status.Id);
            return;
        }

        logger.LogInformation("Updating indexers due to success status from {RunId}", status.Id);
        await search.RunIndexerAll();
    }
}