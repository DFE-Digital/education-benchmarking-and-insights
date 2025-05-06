using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Platform.Test.Mocks;

public static class MockTelemetryClient
{
    public static TelemetryClient Create()
    {
        var mockTelemetryChannel = new MockTelemetryChannel();
        var mockTelemetryConfig = new TelemetryConfiguration
        {
            TelemetryChannel = mockTelemetryChannel,
            ConnectionString = $"InstrumentationKey={Guid.NewGuid()}"
        };

        var mockTelemetryClient = new TelemetryClient(mockTelemetryConfig);
        return mockTelemetryClient;
    }
}

public class MockTelemetryChannel : ITelemetryChannel
{
    private readonly ConcurrentBag<ITelemetry> _sentTelemetries = [];

    public bool? DeveloperMode { get; set; }
    public string? EndpointAddress { get; set; }

    public void Send(ITelemetry item)
    {
        _sentTelemetries.Add(item);
    }

    public void Flush()
    {
    }

    [SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize")]
    public void Dispose()
    {
    }

    public T? GetLastTelemetryItemOfType<T>() => _sentTelemetries.Cast<T>().LastOrDefault();
}