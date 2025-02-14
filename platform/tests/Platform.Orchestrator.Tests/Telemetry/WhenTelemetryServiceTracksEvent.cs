using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Platform.Orchestrator.Telemetry;
using Platform.Test.Mocks;
using Xunit;

namespace Platform.Orchestrator.Tests.Telemetry;

public class WhenTelemetryServiceTracksEvent
{
    private readonly TelemetryService _service;
    private readonly TelemetryClient _telemetryClient;

    public WhenTelemetryServiceTracksEvent()
    {
        _telemetryClient = MockTelemetryClient.Create();
        _service = new TelemetryService(_telemetryClient);
    }

    [Fact]
    public void ShouldTrackEventInTelemetryClient()
    {
        const string eventName = nameof(eventName);
        const string jobId = nameof(jobId);
        var props = new Dictionary<string, string?>
        {
            { "Key", "Value" }
        };

        _service.TrackEvent(eventName, jobId, props);

        var expectedProps = new Dictionary<string, string>
        {
            { "JobId", jobId },
            { "Key", "Value" }
        };
        var channel = _telemetryClient.TelemetryConfiguration.TelemetryChannel as MockTelemetryChannel;
        var telemetry = channel?.GetLastTelemetryItemOfType<EventTelemetry>();
        Assert.NotNull(telemetry);
        Assert.Equal(eventName, telemetry.Name);
        Assert.Equal(expectedProps, telemetry.Properties);
    }
}