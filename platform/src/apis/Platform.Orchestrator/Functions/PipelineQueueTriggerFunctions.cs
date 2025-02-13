using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using Platform.Domain;
using Platform.Domain.Messages;
using Platform.Json;
using Platform.Orchestrator.Extensions;
using Platform.Orchestrator.Sql;
using Platform.Orchestrator.Telemetry;

namespace Platform.Orchestrator.Functions;

public class PipelineQueueTriggerFunctions(ILogger<PipelineQueueTriggerFunctions> logger, IPipelineDb db, ITelemetryService telemetryService)
{
    [Function(nameof(InitiatePipelineJob))]
    public async Task InitiatePipelineJob(
        [QueueTrigger("%PipelineMessageHub:JobPendingQueue%", Connection = "PipelineMessageHub:ConnectionString")] PipelinePending message,
        [DurableClient] DurableTaskClient client)
    {
        using (logger.BeginApplicationScope(message.JobId))
        {
            try
            {
                telemetryService.TrackEvent(Pipeline.Events.PipelinePendingMessageReceived, message.JobId);
                var status = await client.GetInstanceAsync(message.JobId!);

                if (status is not
                    { RuntimeStatus: OrchestrationRuntimeStatus.Pending or OrchestrationRuntimeStatus.Running })
                {
                    await client.ScheduleNewOrchestrationInstanceAsync(nameof(OrchestratorFunctions.PipelineJobOrchestrator), message, new StartOrchestrationOptions(message.JobId));
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
        PipelineFinish job;
        try
        {
            job = message.FromJson<PipelineFinish>();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Finished pipeline job");
            throw;
        }

        using (logger.BeginApplicationScope(job.JobId))
        {
            try
            {
                telemetryService.TrackEvent(Pipeline.Events.PipelineFinishedMessageReceived, job.JobId, new Dictionary<string, string?>
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
}