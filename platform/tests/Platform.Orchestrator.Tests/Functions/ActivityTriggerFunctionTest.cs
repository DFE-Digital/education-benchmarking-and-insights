using Moq;
using Platform.Cache;
using Platform.Orchestrator.Functions;
using Platform.Orchestrator.Search;
using Platform.Orchestrator.Sql;
using Platform.Orchestrator.Telemetry;
using Platform.Test.Mocks;
using Xunit.Abstractions;

namespace Platform.Orchestrator.Tests.Functions;

public abstract class ActivityTriggerFunctionTest
{
    protected ActivityTriggerFunctionTest(ITestOutputHelper testOutputHelper)
    {
        var logger = MockLogger.Create<ActivityTriggerFunctions>(testOutputHelper);
        Database = new Mock<IPipelineDb>();
        Search = new Mock<IPipelineSearch>();
        DistributedCache = new Mock<IDistributedCache>();
        TelemetryService = new Mock<ITelemetryService>();
        TelemetryService
            .Setup(t => t.TrackEvent(It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<Dictionary<string, string?>?>()))
            .Callback<string, string?, Dictionary<string, string?>?>((eventName, jobId, props) =>
            {
                testOutputHelper.WriteLine($"{eventName} Telemetry ({jobId}):");
                if (props == null)
                {
                    return;
                }

                foreach (var (key, value) in props)
                {
                    testOutputHelper.WriteLine($"{key} = {value}");
                }

                testOutputHelper.WriteLine(string.Empty);
            });
        Functions = new ActivityTriggerFunctions(logger.Object, Database.Object, Search.Object, DistributedCache.Object, TelemetryService.Object);
    }

    protected ActivityTriggerFunctions Functions { get; }
    protected Mock<IPipelineDb>? Database { get; set; }
    protected Mock<IPipelineSearch>? Search { get; set; }
    protected Mock<IDistributedCache>? DistributedCache { get; set; }
    protected Mock<ITelemetryService>? TelemetryService { get; set; }
}