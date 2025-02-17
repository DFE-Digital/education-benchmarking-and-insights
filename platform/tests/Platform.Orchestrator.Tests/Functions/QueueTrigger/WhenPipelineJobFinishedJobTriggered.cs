using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask.Client;
using Moq;
using Platform.Domain.Messages;
using Platform.Json;
using Platform.Orchestrator.Functions;
using Xunit;
using Xunit.Abstractions;

namespace Platform.Orchestrator.Tests.Functions.QueueTrigger;

public class WhenPipelineJobFinishedJobTriggered(ITestOutputHelper testOutputHelper) : QueueTriggerFunctionTest(testOutputHelper)
{
    [Fact]
    public async Task ShouldWriteToLog()
    {
        var message = new PipelineFinish
        {
            JobId = "jobId",
            Success = true
        };

        var json = message.ToJson();

        Database?
            .Setup(d => d.WriteToLog(message.JobId, json))
            .Verifiable();

        var client = new Mock<DurableTaskClient>("name");

        await Functions.PipelineJobFinished(json, client.Object);

        Database?.Verify();
    }

    [Fact]
    public async Task ShouldRaiseEvent()
    {
        var message = new PipelineFinish
        {
            JobId = "jobId",
            Success = true
        };

        var client = new Mock<DurableTaskClient>("name");

        client
            .Setup(c => c.RaiseEventAsync(message.JobId, nameof(PipelineQueueTriggerFunctions.PipelineJobFinished), message.Success, It.IsAny<CancellationToken>()))
            .Verifiable();

        await Functions.PipelineJobFinished(message.ToJson(), client.Object);

        client.Verify();
    }

    [Fact]
    public void ShouldReceiveMessagesFromFinishedQueueFromConfiguration()
    {
        var method = typeof(PipelineQueueTriggerFunctions).GetMethod(nameof(PipelineQueueTriggerFunctions.PipelineJobFinished));
        var parameters = method?.GetParameters();
        var messageParam = parameters?.SingleOrDefault(p => p.Name == "message");
        var attribute = messageParam?.GetCustomAttributes(typeof(QueueTriggerAttribute), false).FirstOrDefault() as QueueTriggerAttribute;

        Assert.NotNull(attribute);
        Assert.Equal("PipelineMessageHub:ConnectionString", attribute.Connection);
        Assert.Equal("%PipelineMessageHub:JobFinishedQueue%", attribute.QueueName);
    }
}