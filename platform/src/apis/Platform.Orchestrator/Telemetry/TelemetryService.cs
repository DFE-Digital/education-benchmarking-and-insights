using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationInsights;

namespace Platform.Orchestrator.Telemetry;

public interface ITelemetryService
{
    void TrackEvent(string eventName, string? jobId, Dictionary<string, string?>? props = null);
}

public class TelemetryService(TelemetryClient telemetry) : ITelemetryService
{
    public void TrackEvent(string eventName, string? jobId, Dictionary<string, string?>? props = null)
    {
        if (string.IsNullOrWhiteSpace(jobId))
        {
            return;
        }

        var properties = new Dictionary<string, string>
        {
            { "JobId", jobId }
        };

        if (props != null)
        {
            foreach (var prop in props.Where(p => !string.IsNullOrWhiteSpace(p.Value)))
            {
                properties[prop.Key] = prop.Value!;
            }
        }

        var metrics = new Dictionary<string, double>();
        telemetry.TrackEvent(eventName, properties, metrics);
    }
}