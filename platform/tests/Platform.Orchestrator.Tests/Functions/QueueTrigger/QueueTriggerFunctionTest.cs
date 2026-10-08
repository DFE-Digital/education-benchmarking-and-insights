using Microsoft.Extensions.Options;
using Moq;
using Platform.Orchestrator.Configuration;
using Platform.Orchestrator.Functions;
using Platform.Orchestrator.Sql;
using Platform.Orchestrator.Storage;
using Platform.Orchestrator.Telemetry;
using Xunit.Abstractions;

namespace Platform.Orchestrator.Tests.Functions.QueueTrigger;

public abstract class QueueTriggerFunctionTest
{
    protected QueueTriggerFunctionTest(ITestOutputHelper testOutputHelper)
    {
        Database = new Mock<IPipelineDb>();
        TelemetryService = new Mock<ITelemetryService>();
        StorageService = new Mock<IBlobStorageService>();
        Options = new Mock<IOptions<StorageOptions>>();
        var logger = MockLogger.Create<PipelineQueueTriggerFunctions>(testOutputHelper, TelemetryService);

        Functions = new PipelineQueueTriggerFunctions(logger.Object, Database.Object, TelemetryService.Object, StorageService.Object, Options.Object);
    }

    protected PipelineQueueTriggerFunctions Functions { get; }
    protected Mock<IPipelineDb>? Database { get; }
    private Mock<ITelemetryService>? TelemetryService { get; }
    private Mock<IBlobStorageService>? StorageService { get; }
    private Mock<IOptions<StorageOptions>>? Options { get; }
}
