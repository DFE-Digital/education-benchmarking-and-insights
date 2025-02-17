using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Moq;
using Platform.Domain.Messages;
using Platform.Orchestrator.Functions;
using Xunit;
using Xunit.Abstractions;

namespace Platform.Orchestrator.Tests.Functions.QueueTrigger;

public class WhenInitiatePipelineJobTriggered(ITestOutputHelper testOutputHelper) : QueueTriggerFunctionTest(testOutputHelper)
{
    [Theory]
    [InlineData(OrchestrationRuntimeStatus.Running, false)]
    [InlineData(OrchestrationRuntimeStatus.Completed, true)]
    [InlineData(OrchestrationRuntimeStatus.Pending, false)]
    [InlineData(OrchestrationRuntimeStatus.Suspended, true)]
    public async Task ShouldScheduleNewOrchestrationInstanceIfNotPendingRunning(OrchestrationRuntimeStatus runtimeStatus, bool shouldSchedule)
    {
        var message = new PipelinePending
        {
            JobId = "jobId"
        };

        var status = new OrchestrationMetadata("name", "instanceId")
        {
            RuntimeStatus = runtimeStatus
        };

        var client = new Mock<DurableTaskClient>("name");
        client
            .Setup(c => c.GetInstanceAsync(message.JobId, It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(status);

        client
            .Setup(c => c.ScheduleNewOrchestrationInstanceAsync(nameof(OrchestratorFunctions.PipelineJobOrchestrator), message, It.Is<StartOrchestrationOptions>(o => o.InstanceId == message.JobId), It.IsAny<CancellationToken>()))
            .Verifiable(shouldSchedule ? Times.Once() : Times.Never());

        await Functions.InitiatePipelineJob(message, client.Object);

        client.Verify();
    }

    [Fact]
    public void ShouldReceiveMessagesFromPendingQueueFromConfiguration()
    {
        var method = typeof(PipelineQueueTriggerFunctions).GetMethod(nameof(PipelineQueueTriggerFunctions.InitiatePipelineJob));
        var parameters = method?.GetParameters();
        var messageParam = parameters?.SingleOrDefault(p => p.Name == "message");
        var attribute = messageParam?.GetCustomAttributes(typeof(QueueTriggerAttribute), false).FirstOrDefault() as QueueTriggerAttribute;

        Assert.NotNull(attribute);
        Assert.Equal("PipelineMessageHub:ConnectionString", attribute.Connection);
        Assert.Equal("%PipelineMessageHub:JobPendingQueue%", attribute.QueueName);
    }
}