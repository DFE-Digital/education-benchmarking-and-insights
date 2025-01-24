using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using Platform.Cache;
using Platform.Domain;
using Platform.Domain.Messages;
using Platform.Json;
using Platform.Orchestrator.Search;

namespace Platform.Orchestrator;

public class PipelineFunctions(
    ILogger<PipelineFunctions> logger,
    IPipelineDb db,
    IPipelineSearch search,
    IDistributedCache cache,
    TelemetryClient telemetry)
{
    [Function(nameof(InitiatePipelineJob))]
    public async Task InitiatePipelineJob(
        [QueueTrigger("%PipelineMessageHub:JobPendingQueue%", Connection = "PipelineMessageHub:ConnectionString")] PipelinePending message,
        [DurableClient] DurableTaskClient client)
    {
        using (GetLoggerScope())
        {
            try
            {
                TrackEvent(Pipeline.Events.PipelinePendingMessageReceived, message.JobId);
                var status = await client.GetInstanceAsync(message.JobId!);

                if (status is not
                    { RuntimeStatus: OrchestrationRuntimeStatus.Pending or OrchestrationRuntimeStatus.Running })
                {
                    await client.ScheduleNewOrchestrationInstanceAsync(nameof(PipelineJobOrchestrator), message, new StartOrchestrationOptions(message.JobId));
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "Initiating pipeline job");
                throw;
            }
        }
    }

    [Function(nameof(PipelineJobFinished))]
    public async Task PipelineJobFinished(
        [QueueTrigger("%PipelineMessageHub:JobFinishedQueue%", Connection = "PipelineMessageHub:ConnectionString")] string message,
        [DurableClient] DurableTaskClient client)
    {
        using (GetLoggerScope())
        {
            try
            {
                var job = message.FromJson<PipelineFinish>();
                TrackEvent(Pipeline.Events.PipelineFinishedMessageReceived, job.JobId, new Dictionary<string, string?>
                {
                    { nameof(job.Success), job.Success.ToString() }
                });
                await db.WriteToLog(job.JobId, message);

                if (!string.IsNullOrEmpty(job.JobId))
                {
                    await client.RaiseEventAsync(job.JobId, nameof(PipelineJobFinished), job.Success);
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "Finished pipeline job");
                throw;
            }
        }
    }

    [Function(nameof(PipelineJobOrchestrator))]
    [SuppressMessage("Usage", "CA2208:Instantiate argument exceptions correctly")]
    public async Task PipelineJobOrchestrator([OrchestrationTrigger] TaskOrchestrationContext context)
    {
        using (GetLoggerScope())
        {
            var input = context.GetInput<PipelinePending>();
            if (!context.IsReplaying)
            {
                TrackEvent(Pipeline.Events.PipelinePendingMessageOrchestrated, input?.JobId, new Dictionary<string, string?>
                {
                    { nameof(input.Type), input?.Type }
                });
            }

            switch (input?.Type)
            {
                case Pipeline.JobType.ComparatorSet:
                case Pipeline.JobType.CustomData:
                    await OrchestrateCustomMessage(context, input);
                    break;
                case Pipeline.JobType.Default:
                    await OrchestrateDefaultMessage(context, input);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(PipelinePending.Type));
            }
        }
    }

    private async Task OrchestrateDefaultMessage(TaskOrchestrationContext context, PipelinePending input)
    {
        var message = PipelineStartDefault.FromPending(input);
        await context.CallActivityAsync(nameof(OnStartDefaultJobTrigger), message);

        logger.LogInformation("Waiting for finished event for default message {JobId}", message.JobId);
        var success = await context.WaitForExternalEvent<bool>(nameof(PipelineJobFinished));
        logger.LogInformation("Received finished event for default message {JobId}", message.JobId);

        await context.CallActivityAsync(nameof(RunIndexerTrigger), new PipelineStatus
        {
            Id = message.JobId,
            Success = success
        });

        await context.CallActivityAsync(nameof(ClearCacheTrigger), new PipelineStatus
        {
            Id = message.RunId.ToString(),
            Success = success
        });
    }

    private async Task OrchestrateCustomMessage(TaskOrchestrationContext context, PipelinePending input)
    {
        var message = PipelineStartCustom.FromPending(input);
        await context.CallActivityAsync(nameof(OnStartCustomJobTrigger), message);

        logger.LogInformation("Waiting for finished event for custom message {JobId}", message.JobId);
        var success = await context.WaitForExternalEvent<bool>(nameof(PipelineJobFinished));
        logger.LogInformation("Received finished event for custom message {JobId}", message.JobId);

        await context.CallActivityAsync(nameof(UpdateStatusTrigger), new PipelineStatus
        {
            Id = message.RunId,
            Success = success
        });
    }

    /// <summary>
    ///     Messages on the <c>data-pipeline-job-default-start</c> queue should already have been scheduled in the Orchestrator
    ///     and thus have a <see cref="PipelineStart.JobId">JobId</see> in the payload. Without this property the
    ///     Orchestrator will not raise the 'Finished' event at a later date from a message on the
    ///     <c>data-pipeline-job-finished</c> queue. Messages should therefore be added to the <c>data-pipeline-job-pending</c>
    ///     queue instead of <c>data-pipeline-job-start</c> directly so that this function is triggered to forward the
    ///     message in the correct format via <see cref="PipelineJobOrchestrator" />.
    /// </summary>
    [Function(nameof(OnStartDefaultJobTrigger))]
    [QueueOutput("%PipelineMessageHub:JobDefaultStartQueue%", Connection = "PipelineMessageHub:ConnectionString")]
    public string[] OnStartDefaultJobTrigger([ActivityTrigger] PipelineStartDefault message)
    {
        using (GetLoggerScope())
        {
            TrackEvent(Pipeline.Events.PipelineStartDefaultMessageReceived, message.JobId);
            logger.LogInformation("Forwarding {JobId} to {StartQueue} start queue", message.JobId, "default");
            return [message.ToJson()];
        }
    }

    [Function(nameof(OnStartCustomJobTrigger))]
    [QueueOutput("%PipelineMessageHub:JobCustomStartQueue%", Connection = "PipelineMessageHub:ConnectionString")]
    public string[] OnStartCustomJobTrigger([ActivityTrigger] PipelineStartCustom message)
    {
        using (GetLoggerScope())
        {
            TrackEvent(Pipeline.Events.PipelineStartCustomMessageReceived, message.JobId);
            logger.LogInformation("Forwarding {JobId} to {StartQueue} start queue", message.JobId, "custom");
            return [message.ToJson()];
        }
    }

    [Function(nameof(UpdateStatusTrigger))]
    public async Task UpdateStatusTrigger([ActivityTrigger] PipelineStatus status)
    {
        using (GetLoggerScope())
        {
            TrackEvent(Pipeline.Events.PipelineStatusReceived, status.Id, new Dictionary<string, string?>
            {
                { "Action", "UpdateStatus" }
            });

            logger.LogInformation("Updating status for {RunId}", status.Id);
            await db.UpdateStatus(status);
        }
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
        using (GetLoggerScope())
        {
            TrackEvent(Pipeline.Events.PipelineStatusReceived, status.Id, new Dictionary<string, string?>
            {
                { "Action", "RunIndexer" }
            });

            if (!status.Success)
            {
                logger.LogInformation("Not updating indexers due to failed status from {RunId}", status.Id);
                return;
            }

            logger.LogInformation("Updating indexers due to success status from {RunId}", status.Id);
            await search.RunIndexerAll();
        }
    }

    [Function(nameof(ClearCacheTrigger))]
    public async Task ClearCacheTrigger([ActivityTrigger] PipelineStatus status)
    {
        using (GetLoggerScope())
        {
            TrackEvent(Pipeline.Events.PipelineStatusReceived, status.Id, new Dictionary<string, string?>
            {
                { "Action", "ClearCache" }
            });

            if (!status.Success)
            {
                logger.LogInformation("Not clearing keys from distributed cache due to failed status for {RunId}", status.Id);
                return;
            }

            logger.LogInformation("Clearing keys from distributed cache after completion of {RunId}", status.Id);
            await cache.DeleteAsync($"{status.Id}:*");
        }
    }

    private void TrackEvent(string eventName, string? id, Dictionary<string, string?>? props = null)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return;
        }

        var properties = new Dictionary<string, string>
        {
            { "Id", id }
        };

        if (props != null)
        {
            foreach (var prop in props.Where(p => !string.IsNullOrWhiteSpace(p.Value)))
            {
                properties[prop.Key] = prop.Value!;
            }
        }

        var metrics = new Dictionary<string, double>();
        telemetry.TrackEvent(eventName, properties, metrics);
    }

    private IDisposable? GetLoggerScope() => logger.BeginScope(new Dictionary<string, object>
    {
        { "Application", Constants.ApplicationName }
    });
}