using System.Net;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Platform.MaintenanceTasks.Features.MonitorCommercialResources;
using Platform.MaintenanceTasks.Features.MonitorCommercialResources.Models;
using Platform.Sql;
using Platform.Test.Mocks;
using Xunit;

namespace Platform.MaintenanceTasks.Tests.MonitorCommercialResources;

public class MonitorCommercialResourcesFunctionTests
{
    private const string EventName = "commercial-resource-check";

    [Theory]
    [InlineData("Foo", "https://foo.com", true, HttpStatusCode.OK)]
    [InlineData("Bar", "https://bar.com", false, HttpStatusCode.NotFound)]
    [InlineData("Baz", "https://baz.com", false, HttpStatusCode.Unauthorized)]
    public void LogsExpectedTelemetryBasedOnResult(
        string title,
        string url,
        bool success,
        HttpStatusCode? statusCode)
    {
        var result = new CommercialResourceResult
        {
            Title = title,
            Url = url,
            Success = success,
            StatusCode = statusCode,
            RedirectLocation = null,
            Exception = null
        };

        var (function, channel) = SetupFunctionForTelemetryTests();

        function.LogResult(result);

        var eventTelemetry = channel?.GetLastTelemetryItemOfType<EventTelemetry>();
        Assert.NotNull(eventTelemetry);

        Assert.Equal(EventName, eventTelemetry.Name);
        Assert.Equal(title, eventTelemetry.Properties["Title"]);
        Assert.Equal(url, eventTelemetry.Properties["Url"]);
        Assert.Equal(statusCode.ToString(), eventTelemetry.Properties["StatusCode"]);
        Assert.Equal(success.ToString(), eventTelemetry.Properties["Success"]);
        Assert.Equal("none", eventTelemetry.Properties["RedirectLocation"]);
        Assert.Equal("none", eventTelemetry.Properties["Exception"]);
    }

    [Fact]
    public void LogsExpectedTelemetryBasedOnResultWhenRedirect()
    {
        const string title = "Foo";
        const string url = "https://foo.com";
        const string redirectLocation = "https://bar.com/";
        const bool success = false;
        const HttpStatusCode statusCode = HttpStatusCode.MovedPermanently;

        var result = new CommercialResourceResult
        {
            Title = title,
            Url = url,
            Success = success,
            StatusCode = statusCode,
            RedirectLocation = redirectLocation,
            Exception = null
        };

        var (function, channel) = SetupFunctionForTelemetryTests();

        function.LogResult(result);

        var eventTelemetry = channel?.GetLastTelemetryItemOfType<EventTelemetry>();
        Assert.NotNull(eventTelemetry);

        Assert.Equal(EventName, eventTelemetry.Name);
        Assert.Equal(title, eventTelemetry.Properties["Title"]);
        Assert.Equal(url, eventTelemetry.Properties["Url"]);
        Assert.Equal(statusCode.ToString(), eventTelemetry.Properties["StatusCode"]);
        Assert.Equal(success.ToString(), eventTelemetry.Properties["Success"]);
        Assert.Equal(redirectLocation, eventTelemetry.Properties["RedirectLocation"]);
        Assert.Equal("none", eventTelemetry.Properties["Exception"]);
    }

    [Fact]
    public void LogsExpectedTelemetryBasedOnResultWhenException()
    {
        const string title = "Foo";
        const string url = "https://foo.com";
        const bool success = false;

        var result = new CommercialResourceResult
        {
            Title = title,
            Url = url,
            Success = success,
            StatusCode = null,
            RedirectLocation = null,
            Exception = new HttpRequestException("TestException")
        };

        var (function, channel) = SetupFunctionForTelemetryTests();

        function.LogResult(result);

        var eventTelemetry = channel?.GetLastTelemetryItemOfType<EventTelemetry>();
        Assert.NotNull(eventTelemetry);

        Assert.Equal(EventName, eventTelemetry.Name);
        Assert.Equal(title, eventTelemetry.Properties["Title"]);
        Assert.Equal(url, eventTelemetry.Properties["Url"]);
        Assert.Equal("none", eventTelemetry.Properties["StatusCode"]);
        Assert.Equal(success.ToString(), eventTelemetry.Properties["Success"]);
        Assert.Equal("none", eventTelemetry.Properties["RedirectLocation"]);
        Assert.Contains("TestException", eventTelemetry.Properties["Exception"]);
    }

    [Theory]
    [InlineData("Foo", "https://foo.com", HttpStatusCode.OK, true)]
    [InlineData("Bar", "https://bar.com", HttpStatusCode.NotFound, false)]
    [InlineData("Baz", "https://baz.com", HttpStatusCode.Unauthorized, false)]
    public async Task CheckResourceAsyncReturnsCorrectResult(string title, string url, HttpStatusCode statusCode, bool success)
    {
        var resource = new CommercialResource
        {
            Title = title,
            Url = url
        };

        var function = SetupFunctionForResourceCheckTests(statusCode);

        var result = await function.CheckResourceAsync(resource);

        Assert.Equal(success, result.Success);
        Assert.Equal(title, result.Title);
        Assert.Equal(url, result.Url);
        Assert.Equal(statusCode, result.StatusCode);
        Assert.Null(result.RedirectLocation);
        Assert.Null(result.Exception);
    }

    [Fact]
    public async Task CheckResourceAsyncReturnsCorrectResultWhenRedirect()
    {
        const string title = "Foo";
        const string url = "https://foo.com";
        const string expectedRedirectLocation = "https://bar.com/";
        const HttpStatusCode statusCode = HttpStatusCode.MovedPermanently;

        var resource = new CommercialResource
        {
            Title = title,
            Url = url
        };

        var function = SetupFunctionForResourceCheckTests(statusCode, expectedRedirectLocation);

        var result = await function.CheckResourceAsync(resource);

        Assert.False(result.Success);
        Assert.Equal(title, result.Title);
        Assert.Equal(url, result.Url);
        Assert.Equal(statusCode, result.StatusCode);
        Assert.Equal(expectedRedirectLocation, result.RedirectLocation);
        Assert.Null(result.Exception);
    }

    [Fact]
    public async Task CheckResourceAsyncReturnsCorrectResultWhenException()
    {
        const string title = "Foo";
        const string url = "https://foo.com";

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("TestException"));

        var httpClient = new HttpClient(handlerMock.Object);
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        httpClientFactoryMock.Setup(f => f.CreateClient("NoRedirects")).Returns(httpClient);

        var dbFactoryMock = new Mock<IDatabaseFactory>();

        var loggerMock = new Mock<ILogger<MonitorCommercialResourcesFunction>>();

        var telemetryClient = MockTelemetryClient.Create();

        var function = new MonitorCommercialResourcesFunction(
            dbFactoryMock.Object,
            httpClientFactoryMock.Object,
            loggerMock.Object,
            telemetryClient);

        var resource = new CommercialResource
        {
            Title = title,
            Url = url
        };

        var result = await function.CheckResourceAsync(resource);

        Assert.False(result.Success);
        Assert.Equal(title, result.Title);
        Assert.Equal(url, result.Url);
        Assert.Null(result.StatusCode);
        Assert.Null(result.RedirectLocation);
        Assert.NotNull(result.Exception);
        Assert.Equal("TestException", result.Exception.Message);
    }

    private static MonitorCommercialResourcesFunction SetupFunctionForResourceCheckTests(HttpStatusCode statusCode, string? mockRedirectLocation = null)
    {
        var response = new HttpResponseMessage(statusCode);

        if (!string.IsNullOrEmpty(mockRedirectLocation))
        {
            response.Headers.Location = new Uri(mockRedirectLocation);
        }

        var dbFactoryMock = new Mock<IDatabaseFactory>();

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);
        var httpClient = new HttpClient(handlerMock.Object);
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        httpClientFactoryMock.Setup(f => f.CreateClient("NoRedirects")).Returns(httpClient);

        var loggerMock = new Mock<ILogger<MonitorCommercialResourcesFunction>>();

        var telemetryClient = MockTelemetryClient.Create();


        var function = new MonitorCommercialResourcesFunction(
            dbFactoryMock.Object,
            httpClientFactoryMock.Object,
            loggerMock.Object,
            telemetryClient);

        return function;
    }

    private static (MonitorCommercialResourcesFunction function, MockTelemetryChannel? channel) SetupFunctionForTelemetryTests()
    {
        var dbFactoryMock = new Mock<IDatabaseFactory>();

        var handlerMock = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(handlerMock.Object);
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        httpClientFactoryMock.Setup(f => f.CreateClient("NoRedirects")).Returns(httpClient);

        var loggerMock = new Mock<ILogger<MonitorCommercialResourcesFunction>>();

        var telemetryClient = MockTelemetryClient.Create();

        var function = new MonitorCommercialResourcesFunction(
            dbFactoryMock.Object,
            httpClientFactoryMock.Object,
            loggerMock.Object,
            telemetryClient);

        var channel = telemetryClient.TelemetryConfiguration.TelemetryChannel as MockTelemetryChannel;

        return (function, channel);
    }
}