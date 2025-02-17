using Moq;
using Platform.Orchestrator.Functions;
using Platform.Orchestrator.Sql;
using Platform.Orchestrator.Telemetry;
using Xunit.Abstractions;

namespace Platform.Orchestrator.Tests.Functions.QueueTrigger;

public abstract class QueueTriggerFunctionTest
{
    protected QueueTriggerFunctionTest(ITestOutputHelper testOutputHelper)
    {
        Database = new Mock<IPipelineDb>();
        TelemetryService = new Mock<ITelemetryService>();
        var logger = MockLogger.Create<PipelineQueueTriggerFunctions>(testOutputHelper, TelemetryService);

        Functions = new PipelineQueueTriggerFunctions(logger.Object, Database.Object, TelemetryService.Object);
    }

    protected PipelineQueueTriggerFunctions Functions { get; }
    protected Mock<IPipelineDb>? Database { get; }
    private Mock<ITelemetryService>? TelemetryService { get; }
}