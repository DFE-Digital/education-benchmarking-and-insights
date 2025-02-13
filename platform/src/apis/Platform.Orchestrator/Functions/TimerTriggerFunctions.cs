using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using Platform.Orchestrator.Extensions;

namespace Platform.Orchestrator.Functions;

public static class TimerTriggerFunctions
{
    [Function(nameof(PipelineJobPurgeHistory))]
    public static Task PipelineJobPurgeHistory(
        [DurableClient] DurableTaskClient client,
        [TimerTrigger("0 0 12 * * *")] TimerInfo timer,
        FunctionContext context)
    {
        var logger = context.GetLogger(nameof(PipelineJobPurgeHistory));
        using (logger.BeginApplicationScope())
        {
            logger.LogInformation("Starting to purge expired jobs");
            return client.PurgeAllInstancesAsync(
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
    }
}