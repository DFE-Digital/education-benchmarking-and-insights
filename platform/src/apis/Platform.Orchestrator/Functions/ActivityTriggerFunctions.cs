using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Platform.Cache;
using Platform.Domain;
using Platform.Domain.Messages;
using Platform.Json;
using Platform.Orchestrator.Extensions;
using Platform.Orchestrator.Search;
using Platform.Orchestrator.Sql;
using Platform.Orchestrator.Telemetry;

// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Platform.Orchestrator.Functions;

public class ActivityTriggerFunctions(
    ILogger<ActivityTriggerFunctions> logger,
    IPipelineDb db,
    IPipelineSearch search,
    IDistributedCache cache,
    ITelemetryService telemetryService)
{
    /// <summary>
    ///     Messages on the <c>data-pipeline-job-default-start</c> queue should already have been scheduled in the Orchestrator
    ///     and thus have a <see cref="PipelineStart.JobId">JobId</see> in the payload. Without this property the
    ///     Orchestrator will not raise the 'Finished' event at a later date from a message on the
    ///     <c>data-pipeline-job-finished</c> queue. Messages should therefore be added to the <c>data-pipeline-job-pending</c>
    ///     queue instead of <c>data-pipeline-job-start</c> directly so that this function is triggered to forward the
    ///     message in the correct format via <see cref="OrchestratorFunctions.PipelineJobOrchestrator" />.
    /// </summary>
    [Function(nameof(OnStartDefaultJobTrigger))]
    [QueueOutput("%PipelineMessageHub:JobDefaultStartQueue%", Connection = "PipelineMessageHub:ConnectionString")]
    public string[] OnStartDefaultJobTrigger([ActivityTrigger] PipelineStartDefault message)
    {
        using (logger.BeginApplicationScope(message.JobId))
        {
            telemetryService.TrackEvent(Pipeline.Events.PipelineStartDefaultMessageReceived, message.JobId);
            logger.LogInformation("Forwarding {JobId} to {StartQueue} start queue", message.JobId, "default");
            return [message.ToJson()];
        }
    }

    [Function(nameof(OnStartCustomJobTrigger))]
    [QueueOutput("%PipelineMessageHub:JobCustomStartQueue%", Connection = "PipelineMessageHub:ConnectionString")]
    public string[] OnStartCustomJobTrigger([ActivityTrigger] PipelineStartCustom message)
    {
        using (logger.BeginApplicationScope(message.JobId))
        {
            telemetryService.TrackEvent(Pipeline.Events.PipelineStartCustomMessageReceived, message.JobId);
            logger.LogInformation("Forwarding {JobId} to {StartQueue} start queue", message.JobId, "custom");
            return [message.ToJson()];
        }
    }

    [Function(nameof(UpdateStatusTrigger))]
    public async Task UpdateStatusTrigger([ActivityTrigger] PipelineStatus status)
    {
        using (logger.BeginApplicationScope(status.JobId))
        {
            telemetryService.TrackEvent(Pipeline.Events.PipelineStatusReceived, status.JobId, new Dictionary<string, string?>
            {
                { "Action", "UpdateStatus" },
                { "Skip", (!status.Success).ToString() }
            });

            logger.LogInformation("Updating status for {RunId}", status.RunId);
            var rowsAffected = await db.UpdateUserDataStatus(status);
            logger.LogInformation("Finished updating status for {RunId} ({RowsAffected} row(s) affected)", status.RunId, rowsAffected);
        }
    }

    [Function(nameof(RunIndexerTrigger))]
    public async Task RunIndexerTrigger([ActivityTrigger] PipelineStatus status)
    {
        using (logger.BeginApplicationScope(status.JobId))
        {
            telemetryService.TrackEvent(Pipeline.Events.PipelineStatusReceived, status.JobId, new Dictionary<string, string?>
            {
                { "Action", "RunIndexer" },
                { "Skip", (!status.Success).ToString() }
            });

            if (!status.Success)
            {
                logger.LogInformation("Not updating indexers due to failed status for {RunId}", status.RunId);
                return;
            }

            logger.LogInformation("Updating indexers due to success status for {RunId}", status.RunId);
            var success = await search.RunIndexerAll();
            logger.LogInformation("Finished updating indexers for {RunId} ({Success})", status.RunId, success ? "success" : "failure");
        }
    }

    [Function(nameof(ClearCacheTrigger))]
    public async Task ClearCacheTrigger([ActivityTrigger] PipelineStatus status)
    {
        using (logger.BeginApplicationScope(status.JobId))
        {
            telemetryService.TrackEvent(Pipeline.Events.PipelineStatusReceived, status.JobId, new Dictionary<string, string?>
            {
                { "Action", "ClearCache" },
                { "Skip", (!status.Success).ToString() }
            });

            if (!status.Success)
            {
                logger.LogInformation("Not clearing keys from distributed cache due to failed status for {RunId}", status.RunId);
                return;
            }

            logger.LogInformation("Clearing keys from distributed cache after completion for {RunId}", status.RunId);
            await cache.DeleteAsync($"{status.RunId}:*");
        }
    }

    [Function(nameof(DeactivateUserDataTrigger))]
    public async Task DeactivateUserDataTrigger([ActivityTrigger] PipelineStatus status)
    {
        using (logger.BeginApplicationScope(status.JobId))
        {
            telemetryService.TrackEvent(Pipeline.Events.PipelineStatusReceived, status.JobId, new Dictionary<string, string?>
            {
                { "Action", "DeactivateUserData" },
                { "Skip", (!status.Success).ToString() }
            });

            if (!status.Success)
            {
                logger.LogInformation("Not deactivating user data due to failed status for {RunId}", status.RunId);
                return;
            }

            logger.LogInformation("Deactivating user data due to success status for {RunId}", status.RunId);
            var rowsAffected = await db.DeactivateUserData();
            logger.LogInformation("Finished deactivating user data for {RunId} ({Rows} row(s) affected)", status.RunId, rowsAffected);
        }
    }
}

public record PipelineStatus
{
    public string? JobId { get; set; }
    public string? RunId { get; set; }
    public bool Success { get; set; }
}