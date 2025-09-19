using System.Net;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Logging;
using Moq;
using Platform.MaintenanceTasks.Features.MonitorCommercialResources;
using Platform.MaintenanceTasks.Features.MonitorCommercialResources.Models;
using Platform.MaintenanceTasks.Features.MonitorCommercialResources.Services;
using Platform.Test.Mocks;
using Xunit;

namespace Platform.MaintenanceTasks.Tests.MonitorCommercialResources;

public class MonitorCommercialResourcesFunctionTests
{
    [Theory]
    [MemberData(nameof(ResourceCheckScenarios))]
    public async Task LogsExpectedTelemetryBasedOnResult(
        string title,
        string url,
        bool success,
        HttpStatusCode? statusCode,
        string? redirectLocation,
        string expectedException)
    {
        var resource = new CommercialResource
        {
            Title = title,
            Url = url
        };

        var result = new CommercialResourceResult
        {
            Title = title,
            Url = url,
            Success = success,
            StatusCode = statusCode,
            RedirectLocation = redirectLocation,
            Exception = expectedException == "none"
                ? null
                : new HttpRequestException("Test")
        };

        var serviceMock = new Mock<IMonitorCommercialResourcesService>();
        serviceMock.Setup(s => s.GetResourcesAsync())
                   .ReturnsAsync([resource]);

        serviceMock.Setup(s => s.CheckResourceAsync(resource))
                   .ReturnsAsync(result);

        var loggerMock = new Mock<ILogger<MonitorCommercialResourcesFunction>>();
        var telemetryClient = MockTelemetryClient.Create();

        var function = new MonitorCommercialResourcesFunction(
            loggerMock.Object,
            serviceMock.Object,
            telemetryClient);

        var channel = telemetryClient.TelemetryConfiguration.TelemetryChannel as MockTelemetryChannel;

        await function.RunAsync(null!);

        Assert.NotNull(channel);
        var eventTelemetry = channel.GetLastTelemetryItemOfType<EventTelemetry>();
        Assert.NotNull(eventTelemetry);

        Assert.Equal("commercial-resource-check", eventTelemetry.Name);
        Assert.Equal(success.ToString(), eventTelemetry.Properties["Success"]);
        Assert.Equal(statusCode?.ToString() ?? "none", eventTelemetry.Properties["StatusCode"]);
        Assert.Equal(expectedException, eventTelemetry.Properties["Exception"]);
    }

    public static TheoryData<string, string, bool, HttpStatusCode?, string?, string> ResourceCheckScenarios => new()
    {
        { "Foo", "https://foo.com", true, HttpStatusCode.OK, null, "none" },
        { "Bar", "https://bar.com", false, HttpStatusCode.NotFound, null, "none" },
        { "Baz", "https://baz.com", false, HttpStatusCode.MovedPermanently, "https://baz-new.com", "none" },
        { "Qux", "https://qux.com", false, null, null, "System.Net.Http.HttpRequestException: Test" }
    };
}