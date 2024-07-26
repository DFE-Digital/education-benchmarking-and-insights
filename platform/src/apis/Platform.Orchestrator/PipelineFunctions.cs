using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using Platform.Functions;
using Platform.Functions.Extensions;
namespace Platform.Orchestrator;

public class PipelineFunctions(ILogger<PipelineFunctions> logger, IPipelineDb db)
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
        await context.CallActivityAsync(nameof(OnStartJobTrigger), input);

        logger.LogInformation("{JobId} waiting for finished event", input?.JobId);
        var success = await context.WaitForExternalEvent<bool>(nameof(PipelineJobFinished));
        logger.LogInformation("{JobId} received finished event", input?.JobId);

        switch (input?.Type)
        {
            case "comparator-set":
            case "custom-data":
                await context.CallActivityAsync(nameof(UpdateStatusTrigger), new PipelineStatus
                {
                    Id = input.RunId,
                    Success = success
                });
                break;
        }
    }

    [Function(nameof(OnStartJobTrigger))]
    [QueueOutput("%PipelineMessageHub:JobStartQueue%", Connection = "PipelineMessageHub:ConnectionString")]
    public string[] OnStartJobTrigger([ActivityTrigger] PipelineStartMessage message) => [message.ToJson()];

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
}