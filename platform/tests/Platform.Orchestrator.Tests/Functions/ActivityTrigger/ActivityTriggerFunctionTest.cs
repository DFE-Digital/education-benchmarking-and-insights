using Moq;
using Platform.Cache;
using Platform.Orchestrator.Functions;
using Platform.Orchestrator.Search;
using Platform.Orchestrator.Sql;
using Platform.Orchestrator.Telemetry;
using Xunit.Abstractions;

namespace Platform.Orchestrator.Tests.Functions.ActivityTrigger;

public abstract class ActivityTriggerFunctionTest
{
    protected ActivityTriggerFunctionTest(ITestOutputHelper testOutputHelper)
    {
        Database = new Mock<IPipelineDb>();
        Search = new Mock<IPipelineSearch>();
        DistributedCache = new Mock<IDistributedCache>();
        TelemetryService = new Mock<ITelemetryService>();
        var logger = MockLogger.Create<ActivityTriggerFunctions>(testOutputHelper, TelemetryService);

        Functions = new ActivityTriggerFunctions(logger.Object, Database.Object, Search.Object, DistributedCache.Object, TelemetryService.Object);
    }

    protected ActivityTriggerFunctions Functions { get; }
    protected Mock<IPipelineDb>? Database { get; }
    protected Mock<IPipelineSearch>? Search { get; }
    protected Mock<IDistributedCache>? DistributedCache { get; }
    private Mock<ITelemetryService>? TelemetryService { get; }
}