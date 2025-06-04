using Xunit;

namespace Platform.Functions.Tests;

public class VersionedHandlerDispatcherTests
{
    private class TestHandler(string version) : IVersionedHandler
    {
        public string Version { get; } = version;
    }

    [Fact]
    public void GetHandler_ReturnsCorrectHandler_ForSpecificVersion()
    {
        var v1 = new TestHandler("1.0");
        var v2 = new TestHandler("2.0");
        var dispatcher = new VersionedHandlerDispatcher<TestHandler>([v1, v2]);

        var result = dispatcher.GetHandler("1.0");

        Assert.NotNull(result);
        Assert.Equal("1.0", result.Version);
    }

    [Fact]
    public void GetHandler_ReturnsNull_ForUnsupportedVersion()
    {
        var v1 = new TestHandler("1.0");
        var dispatcher = new VersionedHandlerDispatcher<TestHandler>([v1]);

        var result = dispatcher.GetHandler("9.9");

        Assert.Null(result);
    }

    [Fact]
    public void GetHandler_ReturnsLatestHandler_WhenVersionIsNull()
    {
        var v1 = new TestHandler("1.0");
        var v2 = new TestHandler("2.0");
        var dispatcher = new VersionedHandlerDispatcher<TestHandler>([v1, v2]);

        var result = dispatcher.GetHandler(null);

        Assert.NotNull(result);
        Assert.Equal("2.0", result.Version);
    }

    [Fact]
    public void GetHandler_ReturnsLatestHandler_WhenVersionIsEmpty()
    {
        var v1 = new TestHandler("1.0");
        var v3 = new TestHandler("3.0");
        var dispatcher = new VersionedHandlerDispatcher<TestHandler>([v1, v3]);

        var result = dispatcher.GetHandler("");

        Assert.NotNull(result);
        Assert.Equal("3.0", result.Version);
    }

    [Fact]
    public void Dispatcher_UsesCorrectVersionOrdering()
    {
        var handlers = new List<TestHandler>
        {
            new ("1.0"),
            new ("1.10"),
            new ("2.0"),
            new ("1.9")
        };

        var dispatcher = new VersionedHandlerDispatcher<TestHandler>(handlers);

        var latest = dispatcher.GetHandler(null);

        Assert.NotNull(latest);
        Assert.Equal("2.0", latest.Version);
    }
}