using Microsoft.DurableTask;
using Moq;
using Newtonsoft.Json.Linq;
using Platform.Domain;
using Platform.Domain.Messages;
using Platform.Orchestrator.Functions;
using Xunit;
using Xunit.Abstractions;

namespace Platform.Orchestrator.Tests.Functions.OrchestrationTrigger;

public class WhenPipelineJobOrchestrationTriggeredByDefaultJobType : OrchestrationTriggerFunctionTest
{
    private readonly Mock<TaskOrchestrationContext> _context;
    private readonly PipelinePending _input;

    public WhenPipelineJobOrchestrationTriggeredByDefaultJobType(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _input = new PipelinePending
        {
            Type = Pipeline.JobType.Default,
            JobId = "jobId",
            RunId = "2020",
            Year = JObject.FromObject(new PipelineMessageYears())
        };

        _context = new Mock<TaskOrchestrationContext>();
        _context
            .Setup(c => c.GetInput<PipelinePending>())
            .Returns(_input);
    }

    [Fact]
    public async Task ShouldCallOnStartDefaultJobTriggerActivity()
    {
        _context
            .Setup(c => c.CallActivityAsync(nameof(ActivityTriggerFunctions.OnStartDefaultJobTrigger), It.IsAny<PipelineStartDefault>(), It.IsAny<TaskOptions?>()))
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
    public async Task ShouldCallSubOrchestrator()
    {
        const bool success = true;
        _context
            .Setup(c => c.WaitForExternalEvent<bool>(nameof(PipelineQueueTriggerFunctions.PipelineJobFinished), It.IsAny<CancellationToken>()))
            .ReturnsAsync(success);

        _context
            .Setup(c => c.CallSubOrchestratorAsync(
                nameof(OrchestratorFunctions.PipelineJobDefaultFinished),
                It.Is<PipelineStatus>(p => p.JobId == _input.JobId && p.RunId == _input.RunId!.ToString() && p.Success == success),
                It.IsAny<TaskOptions?>()))
            .Verifiable();

        await Functions.PipelineJobOrchestrator(_context.Object);

        _context.Verify();
    }
}