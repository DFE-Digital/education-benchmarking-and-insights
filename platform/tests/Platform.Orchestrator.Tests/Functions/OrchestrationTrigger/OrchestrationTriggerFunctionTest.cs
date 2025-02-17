using Moq;
using Platform.Orchestrator.Functions;
using Platform.Orchestrator.Telemetry;
using Xunit.Abstractions;

namespace Platform.Orchestrator.Tests.Functions.OrchestrationTrigger;

public abstract class OrchestrationTriggerFunctionTest
{
    protected OrchestrationTriggerFunctionTest(ITestOutputHelper testOutputHelper)
    {
        TelemetryService = new Mock<ITelemetryService>();
        var logger = MockLogger.Create<OrchestratorFunctions>(testOutputHelper, TelemetryService);

        Functions = new OrchestratorFunctions(logger.Object, TelemetryService.Object);
    }

    protected OrchestratorFunctions Functions { get; }
    private Mock<ITelemetryService>? TelemetryService { get; }
}