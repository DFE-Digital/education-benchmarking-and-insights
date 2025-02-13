using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;
using Platform.Domain;
using Platform.Domain.Messages;
using Platform.Orchestrator.Extensions;
using Platform.Orchestrator.Telemetry;

namespace Platform.Orchestrator.Functions;

public class OrchestratorFunctions(ILogger<OrchestratorFunctions> logger, ITelemetryService telemetryService)
{
    [Function(nameof(PipelineJobOrchestrator))]
    [SuppressMessage("Usage", "CA2208:Instantiate argument exceptions correctly")]
    public async Task PipelineJobOrchestrator([OrchestrationTrigger] TaskOrchestrationContext context)
    {
        using (logger.BeginApplicationScope())
        {
            var input = context.GetInput<PipelinePending>();
            if (!context.IsReplaying)
            {
                telemetryService.TrackEvent(Pipeline.Events.PipelinePendingMessageOrchestrated, input?.JobId, new Dictionary<string, string?>
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
        await context.CallActivityAsync(nameof(ActivityTriggerFunctions.OnStartDefaultJobTrigger), message);

        logger.LogInformation("Waiting for finished event for default message {JobId}", message.JobId);
        var success = await context.WaitForExternalEvent<bool>(nameof(PipelineQueueTriggerFunctions.PipelineJobFinished));
        logger.LogInformation("Received finished event for default message {JobId}", message.JobId);

        await context.CallActivityAsync(nameof(ActivityTriggerFunctions.RunIndexerTrigger), new PipelineStatus
        {
            JobId = message.JobId,
            Success = success
        });

        await context.CallActivityAsync(nameof(ActivityTriggerFunctions.ClearCacheTrigger), new PipelineStatus
        {
            JobId = message.JobId,
            RunId = message.RunId.ToString(),
            Success = success
        });
    }

    private async Task OrchestrateCustomMessage(TaskOrchestrationContext context, PipelinePending input)
    {
        var message = PipelineStartCustom.FromPending(input);
        await context.CallActivityAsync(nameof(ActivityTriggerFunctions.OnStartCustomJobTrigger), message);

        logger.LogInformation("Waiting for finished event for custom message {JobId}", message.JobId);
        var success = await context.WaitForExternalEvent<bool>(nameof(PipelineQueueTriggerFunctions.PipelineJobFinished));
        logger.LogInformation("Received finished event for custom message {JobId}", message.JobId);

        await context.CallActivityAsync(nameof(ActivityTriggerFunctions.UpdateStatusTrigger), new PipelineStatus
        {
            JobId = message.JobId,
            RunId = message.RunId,
            Success = success
        });
    }
}