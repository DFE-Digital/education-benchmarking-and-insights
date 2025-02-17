using Microsoft.DurableTask;
using Moq;
using Platform.Domain;
using Platform.Domain.Messages;
using Platform.Orchestrator.Functions;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable UnusedType.Global

namespace Platform.Orchestrator.Tests.Functions.OrchestrationTrigger;

public class WhenPipelineJobOrchestrationTriggeredByComparatorSetJobType(ITestOutputHelper testOutputHelper) : WhenPipelineJobOrchestrationTriggeredByCustomJobType(testOutputHelper, Pipeline.JobType.ComparatorSet);

public class WhenPipelineJobOrchestrationTriggeredByCustomDataJobType(ITestOutputHelper testOutputHelper) : WhenPipelineJobOrchestrationTriggeredByCustomJobType(testOutputHelper, Pipeline.JobType.CustomData);

public abstract class WhenPipelineJobOrchestrationTriggeredByCustomJobType : OrchestrationTriggerFunctionTest
{
    private readonly Mock<TaskOrchestrationContext> _context;
    private readonly PipelinePending _input;

    protected WhenPipelineJobOrchestrationTriggeredByCustomJobType(ITestOutputHelper testOutputHelper, string jobType) : base(testOutputHelper)
    {
        _input = new PipelinePending
        {
            Type = jobType,
            JobId = "jobId",
            RunId = "runId",
            Year = 2020
        };

        _context = new Mock<TaskOrchestrationContext>();
        _context
            .Setup(c => c.GetInput<PipelinePending>())
            .Returns(_input);
    }

    [Fact]
    public async Task ShouldCallOnStartCustomJobTriggerActivity()
    {
        _context
            .Setup(c => c.CallActivityAsync(nameof(ActivityTriggerFunctions.OnStartCustomJobTrigger), It.IsAny<PipelineStartCustom>(), It.IsAny<TaskOptions?>()))
            .Verifiable();

        await Functions.PipelineJobOrchestrator(_context.Object);

        _context.Verify();
    }

    [Fact]
    public async Task ShouldWaitForPipelineJobFinished()
    {
        _context
            .Setup(c => c.WaitForExternalEvent<bool>(nameof(PipelineQueueTriggerFunctions.PipelineJobFinished), It.IsAny<CancellationToken>()))
            .Verifiable();

        await Functions.PipelineJobOrchestrator(_context.Object);

        _context.Verify();
    }

    [Fact]
    public async Task ShouldCallUpdateStatusTriggerActivity()
    {
        const bool success = true;
        _context
            .Setup(c => c.WaitForExternalEvent<bool>(nameof(PipelineQueueTriggerFunctions.PipelineJobFinished), It.IsAny<CancellationToken>()))
            .ReturnsAsync(success);

        _context
            .Setup(c => c.CallActivityAsync(
                nameof(ActivityTriggerFunctions.UpdateStatusTrigger),
                It.Is<PipelineStatus>(p => p.JobId == _input.JobId && p.RunId == (string?)_input.RunId && p.Success == success),
                It.IsAny<TaskOptions?>()))
            .Verifiable();

        await Functions.PipelineJobOrchestrator(_context.Object);

        _context.Verify();
    }
}