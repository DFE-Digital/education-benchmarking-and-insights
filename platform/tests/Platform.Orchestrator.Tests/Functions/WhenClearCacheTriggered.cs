using Moq;
using Platform.Orchestrator.Functions;
using Xunit;
using Xunit.Abstractions;

namespace Platform.Orchestrator.Tests.Functions;

public class WhenClearCacheTriggered(ITestOutputHelper testOutputHelper) : ActivityTriggerFunctionTest(testOutputHelper)
{
    [Fact]
    public async Task ShouldDeleteCacheIfSuccessful()
    {
        var status = new PipelineStatus
        {
            JobId = "jobId",
            RunId = "runId",
            Success = true
        };

        DistributedCache?
            .Setup(d => d.DeleteAsync("runId:*"))
            .Verifiable();

        await Functions.ClearCacheTrigger(status);

        Search?.Verify();
    }

    [Fact]
    public async Task ShouldNotDeleteCacheIfUnsuccessful()
    {
        var status = new PipelineStatus
        {
            JobId = "jobId",
            RunId = "runId",
            Success = false
        };

        DistributedCache?
            .Setup(d => d.DeleteAsync(It.IsAny<string>()))
            .Verifiable(Times.Never);

        await Functions.ClearCacheTrigger(status);

        Search?.Verify();
    }
}