using System.Net;
using Moq;
using Platform.Api.Establishment.Schools;
using Xunit;
namespace Platform.Tests.Establishment;

public class WhenFunctionReceivesQuerySchoolsRequest : SchoolsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Service
            .Setup(d => d.QueryAsync(It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>()))
            .ReturnsAsync(Array.Empty<School>());

        var result = await Functions.QuerySchoolsAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.QueryAsync(It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>()))
            .Throws(new Exception());

        var result = await Functions.QuerySchoolsAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}