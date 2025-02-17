using Microsoft.Azure.Functions.Worker;
using Platform.Domain.Messages;
using Platform.Orchestrator.Functions;
using Xunit;
using Xunit.Abstractions;

namespace Platform.Orchestrator.Tests.Functions.ActivityTrigger;

public class WhenPipelineStartCustomJobTriggered(ITestOutputHelper testOutputHelper) : ActivityTriggerFunctionTest(testOutputHelper)
{
    [Fact]
    public void ShouldReturnSerializedMessageArrayForComparatorSetPipelinePayload()
    {
        var message = new PipelineStartCustom
        {
            JobId = "jobId",
            RunId = "runId",
            RunType = "runType",
            Type = "type",
            Year = 2020,
            Payload = new ComparatorSetPipelinePayload
            {
                Set = ["1", "2", "3"]
            }
        };

        var result = Functions.OnStartCustomJobTrigger(message);

        const string expected = """{"runId":"runId","year":2020,"payload":{"_type":"ComparatorSetPipelinePayload","kind":"ComparatorSetPayload","set":["1","2","3"]},"jobId":"jobId","type":"type","runType":"runType"}""";
        Assert.Single(result);
        Assert.Equal(expected, result.Single());
    }

    [Fact]
    public void ShouldReturnSerializedMessageArrayForCustomDataPipelinePayload()
    {
        var message = new PipelineStartCustom
        {
            JobId = "jobId",
            RunId = "runId",
            RunType = "runType",
            Type = "type",
            Year = 2020,
            Payload = new CustomDataPipelinePayload
            {
                AdministrativeSuppliesNonEducationalCosts = 1.23m
            }
        };

        var result = Functions.OnStartCustomJobTrigger(message);

        const string expected = """{"runId":"runId","year":2020,"payload":{"_type":"CustomDataPipelinePayload","kind":"CustomDataPayload","administrativeSuppliesNonEducationalCosts":1.23},"jobId":"jobId","type":"type","runType":"runType"}""";
        Assert.Single(result);
        Assert.Equal(expected, result.Single());
    }

    [Fact]
    public void ShouldSendMessagesToCustomStartQueueFromConfiguration()
    {
        var method = typeof(ActivityTriggerFunctions).GetMethod(nameof(ActivityTriggerFunctions.OnStartCustomJobTrigger));
        var attribute = method?.GetCustomAttributes(typeof(QueueOutputAttribute), false).FirstOrDefault() as QueueOutputAttribute;

        Assert.NotNull(attribute);
        Assert.Equal("PipelineMessageHub:ConnectionString", attribute.Connection);
        Assert.Equal("%PipelineMessageHub:JobCustomStartQueue%", attribute.QueueName);
    }
}