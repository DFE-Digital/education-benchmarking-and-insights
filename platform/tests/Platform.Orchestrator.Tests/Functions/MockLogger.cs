using Microsoft.Extensions.Logging;
using Moq;
using Platform.Orchestrator.Telemetry;
using Xunit.Abstractions;

namespace Platform.Orchestrator.Tests.Functions;

public static class MockLogger
{
    public static Mock<ILogger<T>> Create<T>(ITestOutputHelper testOutputHelper, Mock<ITelemetryService>? telemetryService = null)
    {
        var logger = Test.Mocks.MockLogger.Create<T>(testOutputHelper);

        telemetryService?
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

        return logger;
    }
}