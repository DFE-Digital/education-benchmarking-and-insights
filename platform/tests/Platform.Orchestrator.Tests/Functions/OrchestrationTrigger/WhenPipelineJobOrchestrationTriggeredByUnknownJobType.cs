using Microsoft.DurableTask;
using Moq;
using Platform.Domain.Messages;
using Xunit;
using Xunit.Abstractions;

namespace Platform.Orchestrator.Tests.Functions.OrchestrationTrigger;

public class WhenPipelineJobOrchestrationTriggeredByUnknownJobType : OrchestrationTriggerFunctionTest
{
    private readonly Mock<TaskOrchestrationContext> _context;

    public WhenPipelineJobOrchestrationTriggeredByUnknownJobType(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        var input = new PipelinePending
        {
            Type = "type"
        };

        _context = new Mock<TaskOrchestrationContext>();
        _context
            .Setup(c => c.GetInput<PipelinePending>())
            .Returns(input);
    }

    [Fact]
    public async Task ShouldThrowException()
    {
        var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => Functions.PipelineJobOrchestrator(_context.Object));

        Assert.NotNull(exception);
        Assert.Equal(nameof(PipelinePending.Type), exception.ParamName);
    }
}