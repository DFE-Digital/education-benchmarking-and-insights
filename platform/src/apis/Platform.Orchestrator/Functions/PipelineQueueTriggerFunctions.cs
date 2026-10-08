using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Domain.Messages;
using Platform.Json;
using Platform.Orchestrator.Configuration;
using Platform.Orchestrator.Extensions;
using Platform.Orchestrator.Storage;
using Platform.Orchestrator.Sql;
using Platform.Orchestrator.Telemetry;

namespace Platform.Orchestrator.Functions;

public class PipelineQueueTriggerFunctions(
    ILogger<PipelineQueueTriggerFunctions> logger,
    IPipelineDb db,
    ITelemetryService telemetryService,
    IBlobStorageService blobStorageService,
    IOptions<StorageOptions> storageOptions)
{
    private readonly StorageOptions _storageOptions = storageOptions.Value;

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
                    { nameof(job.Success), job.Success.ToString() },
                    { nameof(job.Error), job.Error }
                });
                await db.WriteToLog(job.JobId, message);

                if (job is { Success: true, DeriveLaaRiskScores: true })
                {
                    const string fileName = "laa_risk_scores_download.csv";
                    var result = await MoveLocalAuthorityRiskFile(job, fileName);

                    logger.LogInformation("Moved LAA file for JobId {JobId}", job.JobId);

                    if (result.Success)
                    {
                        await db.WriteLocalAuthorityRisksFileDetails(new FileRecord
                        {
                            Type = "LocalAuthorityRiskScores",
                            Label = "Download detailed risk data for all schools",
                            FileName = fileName,
                            ValidFrom = DateTimeOffset.UtcNow,
                            RunId = job.RunId!,
                            Filesize = result.FilesizeMb
                        });

                        logger.LogInformation("Wrote LAA file details to database for JobId {JobId}", job.JobId);
                    }
                }

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

    private async Task<BlobMoveResult> MoveLocalAuthorityRiskFile(PipelineFinish job, string fileName)
    {
        // TODO: this could be cleaner
        if (_storageOptions is not
            {
                SourceAccountUri: { } sourceUri,
                SourceContainer: { } sourceContainer,
                DestAccountUri: { } destUri,
                DestContainer: { } destContainer
            })
        {
            logger.LogWarning("Unable to move LAA file for Job {JobId}: StorageOptions are incomplete.", job.JobId);
            return new BlobMoveResult();
        }

        logger.LogInformation(
            "Moving file from source container '{SourceContainer}' to destination container '{DestContainer}' for JobId {JobId}",
            sourceContainer,
            destContainer,
            job.JobId);

        var sourceBlobName = $"default/{job.RunId}/{fileName}";

        var result = await blobStorageService.MoveBlobAsync(
            sourceUri,
            sourceContainer,
            sourceBlobName,
            destUri,
            destContainer,
            fileName);

        if (!result.Success)
        {
            logger.LogError(
                "Failed to move file from '{SourceContainer}' to '{DestContainer}' for JobId {JobId}. Error: {ErrorMessage}",
                result.SourceContainer,
                result.DestContainer,
                job.JobId,
                result.ErrorMessage);

            return result;
        }

        logger.LogInformation(
            "Successfully moved file of size '{FilesizeMb} MB' from source container '{SourceContainer}' to destination container '{DestContainer}' for JobId {JobId}",
            result.FilesizeMb,
            result.SourceContainer,
            result.DestContainer,
            job.JobId);

        return result;
    }
}
