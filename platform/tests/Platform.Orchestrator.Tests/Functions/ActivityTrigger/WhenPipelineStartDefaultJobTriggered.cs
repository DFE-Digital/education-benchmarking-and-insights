using Microsoft.Azure.Functions.Worker;
using Platform.Domain.Messages;
using Platform.Orchestrator.Functions;
using Xunit;
using Xunit.Abstractions;

namespace Platform.Orchestrator.Tests.Functions.ActivityTrigger;

public class WhenPipelineStartDefaultJobTriggered(ITestOutputHelper testOutputHelper) : ActivityTriggerFunctionTest(testOutputHelper)
{
    [Fact]
    public void ShouldReturnSerializedMessageArray()
    {
        var message = new PipelineStartDefault
        {
            JobId = "jobId",
            RunId = 2020,
            RunType = "runType",
            Type = "type",
            Year = new PipelineMessageYears
            {
                Aar = 2021,
                Bfr = 2022,
                Cfr = 2023,
                S251 = 2024
            }
        };

        var result = Functions.OnStartDefaultJobTrigger(message);

        const string expected = """{"runId":2020,"year":{"aar":2021,"cfr":2023,"bfr":2022,"s251":2024},"jobId":"jobId","type":"type","runType":"runType"}""";
        Assert.Single(result);
        Assert.Equal(expected, result.Single());
    }

    [Fact]
    public void ShouldSendMessagesToDefaultStartQueueFromConfiguration()
    {
        var method = typeof(ActivityTriggerFunctions).GetMethod(nameof(ActivityTriggerFunctions.OnStartDefaultJobTrigger));
        var attribute = method?.GetCustomAttributes(typeof(QueueOutputAttribute), false).FirstOrDefault() as QueueOutputAttribute;

        Assert.NotNull(attribute);
        Assert.Equal("PipelineMessageHub:ConnectionString", attribute.Connection);
        Assert.Equal("%PipelineMessageHub:JobDefaultStartQueue%", attribute.QueueName);
    }
}