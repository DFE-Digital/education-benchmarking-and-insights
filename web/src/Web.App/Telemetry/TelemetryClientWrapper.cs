using System.Diagnostics.CodeAnalysis;
using Microsoft.ApplicationInsights;

namespace Web.App.Telemetry;

public interface ITelemetryClientWrapper
{
    void TrackEvent(string eventName, IDictionary<string, string>? properties = null);
}

[ExcludeFromCodeCoverage]
public class TelemetryClientWrapper(TelemetryClient telemetryClient) : ITelemetryClientWrapper
{
    public void TrackEvent(string eventName, IDictionary<string, string>? properties = null)
    {
        telemetryClient.TrackEvent(eventName, properties);
    }
}