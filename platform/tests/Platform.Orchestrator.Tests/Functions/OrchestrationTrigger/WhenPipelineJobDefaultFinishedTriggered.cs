using Microsoft.DurableTask;
using Moq;
using Platform.Orchestrator.Functions;
using Xunit;
using Xunit.Abstractions;

namespace Platform.Orchestrator.Tests.Functions.OrchestrationTrigger;

public class WhenPipelineJobDefaultFinishedTriggered : OrchestrationTriggerFunctionTest
{
    private readonly Mock<TaskOrchestrationContext> _context;
    private readonly PipelineStatus _input;

    public WhenPipelineJobDefaultFinishedTriggered(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _input = new PipelineStatus
        {
            JobId = "jobId",
            RunId = "2020",
            Success = true
        };

        _context = new Mock<TaskOrchestrationContext>();
        _context
            .Setup(c => c.GetInput<PipelineStatus>())
            .Returns(_input);
    }

    [Fact]
    public async Task ShouldCallRunIndexerTriggerActivity()
    {
        _context
            .Setup(c => c.CallActivityAsync(
                nameof(ActivityTriggerFunctions.RunIndexerTrigger),
                _input,
                It.IsAny<TaskOptions?>()))
            .Verifiable();

        await Functions.PipelineJobDefaultFinished(_context.Object);

        _context.Verify();
    }

    [Fact]
    public async Task ShouldCallClearCacheTriggerActivity()
    {
        _context
            .Setup(c => c.CallActivityAsync(
                nameof(ActivityTriggerFunctions.ClearCacheTrigger),
                _input,
                It.IsAny<TaskOptions?>()))
            .Verifiable();

        await Functions.PipelineJobDefaultFinished(_context.Object);

        _context.Verify();
    }

    [Fact]
    public async Task ShouldCallDeactivateUserDataTriggerActivity()
    {
        _context
            .Setup(c => c.CallActivityAsync(
                nameof(ActivityTriggerFunctions.DeactivateUserDataTrigger),
                _input,
                It.IsAny<TaskOptions?>()))
            .Verifiable();

        await Functions.PipelineJobDefaultFinished(_context.Object);

        _context.Verify();
    }

    [Fact]
    public async Task ShouldNotCallTriggerActivitiesIfInvalidInput()
    {
        _context.Reset();
        _context
            .Setup(c => c.CallActivityAsync(It.IsAny<TaskName>(), It.IsAny<object?>(), It.IsAny<TaskOptions?>()))
            .Verifiable(Times.Never());

        await Functions.PipelineJobDefaultFinished(_context.Object);

        _context.Verify();
    }
}