using Platform.Functions.Extensions;
using Platform.Test.Mocks;
using Xunit;

namespace Platform.Functions.Tests.Extensions;

public class WhenHttpRequestDataReadsVersion
{
    private const string ApiVersionHeader = "x-api-version";

    [Fact]
    public void WithHeaderPresent()
    {
        var request = MockHttpRequestData.Create();
        request.Headers.Add(ApiVersionHeader, "1.0");
        var result = request.ReadVersion();
        Assert.Equal("1.0", result);
    }

    [Fact]
    public void WithHeaderAbsent()
    {
        var request = MockHttpRequestData.Create();
        var result = request.ReadVersion();
        Assert.Null(result);
    }
}