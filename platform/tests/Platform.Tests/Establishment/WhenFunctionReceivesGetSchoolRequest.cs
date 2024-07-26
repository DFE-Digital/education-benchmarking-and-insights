using System.Net;
using Moq;
using Platform.Api.Establishment.Schools;
using Xunit;
namespace Platform.Tests.Establishment;

public class WhenFunctionReceivesGetSchoolRequest : SchoolsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Service
            .Setup(d => d.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(new School());

        var result = await Functions.SingleSchoolAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn404OnInvalidRequest()
    {

        Service
            .Setup(d => d.GetAsync(It.IsAny<string>()))
            .ReturnsAsync((School?)null);

        var result = await Functions.SingleSchoolAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.GetAsync(It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions.SingleSchoolAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}