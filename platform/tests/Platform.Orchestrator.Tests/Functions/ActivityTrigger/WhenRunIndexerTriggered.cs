using Moq;
using Platform.Orchestrator.Functions;
using Xunit;
using Xunit.Abstractions;

namespace Platform.Orchestrator.Tests.Functions.ActivityTrigger;

public class WhenRunIndexerTriggered(ITestOutputHelper testOutputHelper) : ActivityTriggerFunctionTest(testOutputHelper)
{
    [Fact]
    public async Task ShouldUpdateStatusInDatabase()
    {
        var status = new PipelineStatus
        {
            JobId = "jobId",
            RunId = "runId",
            Success = true
        };

        Database?
            .Setup(d => d.UpdateUserDataStatus(status))
            .ReturnsAsync(1)
            .Verifiable();

        await Functions.UpdateStatusTrigger(status);

        Database?.Verify();
    }
}