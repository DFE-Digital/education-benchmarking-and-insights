using Microsoft.DurableTask;
using Moq;
using Platform.Domain;
using Platform.Domain.Messages;
using Platform.Orchestrator.Functions;
using Xunit;
using Xunit.Abstractions;

namespace Platform.Orchestrator.Tests.Functions.OrchestrationTrigger;

public class WhenPipelineJobOrchestrationTriggeredByDeriveLAARiskScoresJobType : OrchestrationTriggerFunctionTest
{
    private readonly Mock<TaskOrchestrationContext> _context;
    private readonly PipelinePending _input;

    public WhenPipelineJobOrchestrationTriggeredByDeriveLAARiskScoresJobType(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _input = new PipelinePending
        {
            Type = Pipeline.JobType.DeriveLAARiskScores,
            JobId = "jobId",
            RunId = "2026"
        };

        _context = new Mock<TaskOrchestrationContext>();
        _context
            .Setup(c => c.GetInput<PipelinePending>())
            .Returns(_input);
    }

    [Fact]
    public async Task ShouldCallOnStartDeriveLAARiskScoresJobTriggerActivity()
    {
        _context
            .Setup(c => c.CallActivityAsync<string[]>(nameof(ActivityTriggerFunctions.OnStartDeriveLAARiskScoresJobTrigger), It.IsAny<PipelineStartDeriveLAARiskScores>(), It.IsAny<TaskOptions?>()))
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
    public async Task ShouldCallClearCacheOnSuccess()
    {
        const bool success = true;
        _context
            .Setup(c => c.WaitForExternalEvent<bool>(nameof(PipelineQueueTriggerFunctions.PipelineJobFinished), It.IsAny<CancellationToken>()))
            .ReturnsAsync(success);

        _context
            .Setup(c => c.CallActivityAsync(
                nameof(ActivityTriggerFunctions.ClearCacheTrigger),
                It.Is<PipelineStatus>(p => p.JobId == _input.JobId && p.RunId == _input.RunId!.ToString() && p.Success == success),
                It.IsAny<TaskOptions?>()))
            .Verifiable();

        await Functions.PipelineJobOrchestrator(_context.Object);

        _context.Verify();
    }

    [Fact]
    public async Task ShouldNotCallClearCacheOnFailure()
    {
        const bool success = false;
        _context
            .Setup(c => c.WaitForExternalEvent<bool>(nameof(PipelineQueueTriggerFunctions.PipelineJobFinished), It.IsAny<CancellationToken>()))
            .ReturnsAsync(success);

        await Functions.PipelineJobOrchestrator(_context.Object);

        _context.Verify(c => c.CallActivityAsync(
            nameof(ActivityTriggerFunctions.ClearCacheTrigger),
            It.IsAny<object>(),
            It.IsAny<TaskOptions?>()), Times.Never);
    }

    [Fact]
    public async Task ShouldResolveTypeFromRunTypeIfTypeIsNull()
    {
        var inputWithNullType = new PipelinePending
        {
            Type = null,
            RunType = Pipeline.JobType.DeriveLAARiskScores,
            JobId = "jobId",
            RunId = "2026"
        };

        var contextMock = new Mock<TaskOrchestrationContext>();
        contextMock
            .Setup(c => c.GetInput<PipelinePending>())
            .Returns(inputWithNullType);

        contextMock
            .Setup(c => c.CallActivityAsync<string[]>(nameof(ActivityTriggerFunctions.OnStartDeriveLAARiskScoresJobTrigger), It.IsAny<PipelineStartDeriveLAARiskScores>(), It.IsAny<TaskOptions?>()))
            .Verifiable();

        await Functions.PipelineJobOrchestrator(contextMock.Object);

        contextMock.Verify();
    }
}
