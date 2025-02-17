using Moq;
using Platform.Orchestrator.Functions;
using Xunit;
using Xunit.Abstractions;

namespace Platform.Orchestrator.Tests.Functions;

public class WhenUpdateStatusTriggered(ITestOutputHelper testOutputHelper) : ActivityTriggerFunctionTest(testOutputHelper)
{
    [Fact]
    public async Task ShouldRunSearchIndexerIfSuccessful()
    {
        var status = new PipelineStatus
        {
            JobId = "jobId",
            RunId = "runId",
            Success = true
        };

        Search?
            .Setup(d => d.RunIndexerAll())
            .Verifiable();

        await Functions.RunIndexerTrigger(status);

        Search?.Verify();
    }

    [Fact]
    public async Task ShouldNotRunSearchIndexerIfUnsuccessful()
    {
        var status = new PipelineStatus
        {
            JobId = "jobId",
            RunId = "runId",
            Success = false
        };

        Search?
            .Setup(d => d.RunIndexerAll())
            .Verifiable(Times.Never);

        await Functions.RunIndexerTrigger(status);

        Search?.Verify();
    }
}